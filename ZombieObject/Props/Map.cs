﻿using System;
using System.Collections.Generic;
using System.Text;
using SFML_Engine;
using SFML.System;

namespace ZombiesGame
{ 
    class Map : GameObject
    {
        //Reference to player
        Player _player;

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
        const byte H_DOOR = 2;
        const byte V_DOOR = 3;
        const byte WALL = 4;
        const byte BONUS_SPEED = 5;
        const byte BONUS_LIFE = 6;
        const byte PISTOL_BUY = 7;
        const byte RIFLE_BUY = 8;
        const byte SHOTGUN_BUY = 9;

        //All doors
        Dictionary<Vector2f, Door> _doors = new Dictionary<Vector2f, Door>();

        public Map(byte[,] map, Player p)
        {
            BYTE_MAP = map;
            _player = p;
        }

        public void GenerateMap()
        {
            Frame frm_Map = new Frame();
            Vector2f position;

            //Generate map
            for (int y = 0; y < BYTE_MAP.GetLength(0); y++)
            {
                for (int x = 0; x < BYTE_MAP.GetLength(1); x++)
                {
                    position = new Vector2f(x * SIZE.X, y * SIZE.Y);
                    switch (BYTE_MAP[y, x])
                    {
                        case H_DOOR: _doors.Add(new Vector2f(x, y), new Door(position + GameMath.ScaleVector(SIZE, new Vector2f(2,2), true), SIZE, _player, true)); break;
                        case V_DOOR: _doors.Add(new Vector2f(x, y), new Door(position + GameMath.ScaleVector(SIZE, new Vector2f(2, 2), true), SIZE, _player, false)); break;
                        case GROUND: frm_Map.AddSprite(position, GROUND_TEXCOORDS, SIZE, TEXSIZE); break;
                        case WALL: frm_Map.AddSprite(position, WALL_TEXCOORDS, SIZE, TEXSIZE); break;
                        case BONUS_SPEED:
                            GameState.AddGameObj(new SpeedBonus(position + new Vector2f(50, 75)));
                            frm_Map.AddSprite(position, GROUND_TEXCOORDS, SIZE, TEXSIZE);
                            break;
                        case BONUS_LIFE:
                            GameState.AddGameObj(new LifeBonus(position + new Vector2f(50,75)));
                            frm_Map.AddSprite(position, GROUND_TEXCOORDS, SIZE, TEXSIZE);
                            break;
                        case PISTOL_BUY:
                            GameState.AddGameObj(new PistolBuy(position + new Vector2f(50, 75)));
                            frm_Map.AddSprite(position, GROUND_TEXCOORDS, SIZE, TEXSIZE);
                            break;
                        case RIFLE_BUY:
                            GameState.AddGameObj(new RifleBuy(position + new Vector2f(50, 75)));
                            frm_Map.AddSprite(position, GROUND_TEXCOORDS, SIZE, TEXSIZE);
                            break;
                        case SHOTGUN_BUY:
                            GameState.AddGameObj(new ShotgunBuy(position + new Vector2f(50, 75)));
                            frm_Map.AddSprite(position, GROUND_TEXCOORDS, SIZE, TEXSIZE);
                            break;
                    }
                }
            }

            //Add all doors in game state
            foreach(Door d in _doors.Values)
            {
                GameState.AddGameObj(d);
            }

            //Set up graphic handler
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

                if(BYTE_MAP[y,x] > 1 && BYTE_MAP[y, x] < 5)
                {
                    if(BYTE_MAP[y, x] == V_DOOR || BYTE_MAP[y, x] == H_DOOR)
                    {
                        _doors.TryGetValue(new Vector2f(x, y), out Door d);
                        if(d.PhysicObject.State == PhysicState.NOCLIP)
                        {
                            continue;
                        }
                    }
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
