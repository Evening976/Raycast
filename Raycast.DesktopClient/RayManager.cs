using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Raycast.DesktopClient
{
    internal class RayManager
    {
        private readonly List<Ray> _rays;
        private readonly Texture2D _rayTex, _rayHitTex;
        private readonly short _rayCount;

        private readonly List<Vector2> _uniquePoints;
        private readonly List<float> _uniqueAngles;

        public RayManager(GraphicsDevice graphicsDevice, ContentManager content, short rayCount)
        {
            _rays = new List<Ray>();
            _rayTex = new Texture2D(graphicsDevice, 1, 1);
            _rayTex.SetData(new Color[] { Color.Red });


            _rayHitTex = content.Load<Texture2D>("circle-32");
            this._rayCount = rayCount;

            _uniquePoints = new List<Vector2>();
            _uniqueAngles = new List<float>();
        }

        public void Update(Vector2 mousePos, List<Wall> walls)
        {
            _rays.Clear();
            for (float i = 0; i < 360; i += 360 / _rayCount)
            {
                _rays.Add(new Ray(mousePos, new Vector2((float)Math.Cos((i * Math.PI) / 180), (float)Math.Sin((i * Math.PI) / 180))));
            }

            processHits(mousePos, walls);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 mousePos)
        {
            foreach (Ray ray in _rays)
            {
                ray.Draw(spriteBatch, _rayTex);
            }

            foreach (Vector2 rH in _uniquePoints)
            {
                spriteBatch.Draw(_rayHitTex, new Rectangle((int)rH.X - 8, (int)rH.Y - 8, 16, 16), Color.White);
                Utils.drawLine(spriteBatch, getRayTex(), new Vector2(mousePos.X, mousePos.Y), rH, Color.Black);
            }
        }


        public void addRay(Ray ray) => _rays.Add(ray);
        public List<Ray> getRays() => _rays;
        public Texture2D getRayTex() => _rayTex;

        private void processHits(Vector2 mousePos, List<Wall> walls)
        {
            _uniquePoints.Clear();

            for (int r = 0; r < getRays().Count; r++)
            {
                Vector2 closest = new Vector2();
                float record = float.MaxValue;

                for (int w = 0; w < walls.Count; w++)
                {
                    Vector2 hit = getRays()[r].Cast(walls[w].start, walls[w].end);

                    if (hit != new Vector2(32383773.23f, 32383773.23f))
                    {
                        float d = Vector2.Distance(mousePos, hit);
                        if (d < record)
                        {
                            record = Vector2.Distance(mousePos, hit);
                            closest = hit;
                        }
                    }
                }

                _uniquePoints.Add(closest);

                for (int a = 0; a < _uniquePoints.Count; a++)
                {
                    float angle = (float)Math.Atan2(_uniquePoints[a].Y, _uniquePoints[a].X);
                    _uniqueAngles.Add(angle - (1 / 100000));
                    _uniqueAngles.Add(angle);
                    _uniqueAngles.Add(angle + (1 / 100000));
                }
            }
        }

    }
}
