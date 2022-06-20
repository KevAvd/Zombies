using System;
using System.Collections.Generic;
using System.Text;
using SFML_Engine;
using SFML.System;

namespace ZombiesGame
{ 
    class Map : GameObject
    {
        //Readonly
        readonly Vector2f GROUND_TEXCOORDS = new Vector2f(80, 0);
        readonly Vector2f WALL_TEXCOORDS = new Vector2f(80, 16);
        readonly Vector2f DOOR_TEXCOORDS = new Vector2f(80, 32);
        readonly Vector2f SIZE = new Vector2f(150, 150);
        readonly Vector2f TEXSIZE = new Vector2f(16, 16);
        readonly byte[,] BYTE_MAP;

        //Constant
        const byte VOID = 0;
        const byte GROUND = 1;
        const byte WALL = 2;
        const byte DOOR = 3;

        public Map(byte[,] map)
        {
            BYTE_MAP = map;
            Frame frm_Map = new Frame();
            Vector2f position;
            for(int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    position = new Vector2f(x * SIZE.X, y * SIZE.Y);
                    switch (map[y, x])
                    {
                        case DOOR: frm_Map.AddSprite(position, DOOR_TEXCOORDS, SIZE, TEXSIZE); break;
                        case WALL: frm_Map.AddSprite(position, WALL_TEXCOORDS, SIZE, TEXSIZE); break;
                        case GROUND: frm_Map.AddSprite(position, GROUND_TEXCOORDS, SIZE, TEXSIZE); break;
                    }
                }
            }

            GraphicHandler.Frame = frm_Map;
            GraphicHandler.GraphicState = GraphicState.BACKGROUND;
            GraphicHandler.SetFrameAsCurrent();
        }

        public void ResolveMapCollision(GameObject obj)
        {
            Vector2f[] points = obj.PhysicObject.GetPoints();
            int x;
            int y;
            for(int i = 0; i < points.Length; i++)
            {
                x = (int)Math.Floor(points[i].X / SIZE.X);
                y = (int)Math.Floor(points[i].Y / SIZE.Y);

                if(x < 0 || x >= BYTE_MAP.GetLength(1) || y < 0 || y >= BYTE_MAP.GetLength(1))
                {
                    continue;
                }

                if(BYTE_MAP[y,x] > 1)
                {
                    AABB mapAABB = new AABB(new Vector2f(SIZE.X * x + SIZE.X / 2, SIZE.Y * y + SIZE.Y / 2), SIZE.X, SIZE.Y);
                    Ray ray = new Ray(obj.Position, new Vector2f(SIZE.X * x + SIZE.X / 2, SIZE.Y * y + SIZE.Y / 2));
                    CollisionDetection.AABB_RAY(mapAABB, ray, out Vector2f pNear1, out Vector2f pFar1, out Vector2f normal1);
                    float closeX = Math.Abs(pNear1.X - points[i].X);
                    float closeY = Math.Abs(pNear1.Y - points[i].Y);
                    obj.Position += new Vector2f(normal1.X * closeX, normal1.Y * closeY);
                }
            }
        }
    }
}
