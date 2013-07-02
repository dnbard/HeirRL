using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HeirRL.Level
{
    class MapGrid : DrawableGameComponent
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        private GridElement[,] Grid;

        public MapGrid(int width, int height) : base(Program.Game)
        {
            Width = width;
            Height = height;

            Grid = new GridElement[Width, Height];

            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                    Grid[i,j] = new GridElement(i, j);
            CalculateNearbyElements();
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var gridElement in Grid)
                gridElement.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var gridElement in Grid)
                gridElement.Update(gameTime);
        }

        public GridElement GetGridElement(int x, int y)
        {
            if (x < 0 || x >= Width) return null;
            if (y < 0 || y >= Height) return null;

            return Grid[x, y];
        }

        private void CalculateNearbyElements()
        {
            foreach (var gridElement in Grid)
            {
                bool even = ParityCheck(gridElement.Column);

                int x = gridElement.Row, y = gridElement.Column, i = 0;

                if (!even)
                {
                    gridElement.NearbyElements[i] = GetGridElement(x, y - 2); i++;
                    gridElement.NearbyElements[i] = GetGridElement(x + 1, y - 1); i++;
                    gridElement.NearbyElements[i] = GetGridElement(x + 1, y); i++;
                    gridElement.NearbyElements[i] = GetGridElement(x + 1, y + 1); i++;
                    gridElement.NearbyElements[i] = GetGridElement(x, y + 2); i++;
                    gridElement.NearbyElements[i] = GetGridElement(x, y + 1); i++;
                    gridElement.NearbyElements[i] = GetGridElement(x - 1, y); i++;
                    gridElement.NearbyElements[i] = GetGridElement(x, y - 1); i++;
                }
                else
                {
                    gridElement.NearbyElements[i] = GetGridElement(x, y - 2); i++;
                    gridElement.NearbyElements[i] = GetGridElement(x, y - 1); i++;
                    gridElement.NearbyElements[i] = GetGridElement(x + 1, y); i++;
                    gridElement.NearbyElements[i] = GetGridElement(x, y + 1); i++;
                    gridElement.NearbyElements[i] = GetGridElement(x, y + 2); i++;
                    gridElement.NearbyElements[i] = GetGridElement(x - 1, y + 1); i++;
                    gridElement.NearbyElements[i] = GetGridElement(x - 1, y); i++;
                    gridElement.NearbyElements[i] = GetGridElement(x - 1, y - 1); i++;
                }
            }
        }

        private static bool ParityCheck(int val)
        {
            return (val % 2 == 0);
        }
    }
}
