﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallDefense
{
    class Map
    {
        //Static variables
        private static Map mapMaster;
        public static Map mapCurrent;

        //Fields of any Map object
        private int iVisualMapWidth = 0;
        private int iVisualMapHeight = 0;
        private int iTrueMapWidth = 0;
        private int iTrueMapHeight = 0;
        private MapTile[][] mtMapData;

        public Map(string[][] _sMapJagged)
        {
            iVisualMapWidth = (_sMapJagged[0].Count() * 2) + 1;
            iVisualMapHeight = _sMapJagged.Count();
            iTrueMapWidth = _sMapJagged[0].Count();
            iTrueMapHeight = _sMapJagged.Count();
            mtMapData = new MapTile[iVisualMapHeight][];

            for (int row = 0; row < _sMapJagged.Length; row++)
            {
                mtMapData[row] = new MapTile[_sMapJagged[0].Length];
                for (int col = 0; col < _sMapJagged[0].Length; col++)
                {
                    mtMapData[row][col] = new MapTile(_sMapJagged[row][col]);
                }
            }
        }

        //Properties
        public int VisualMapWidth
        {
            get
            {
                return iVisualMapWidth;
            }
        }

        public int VisualMapHeight
        {
            get
            {
                return iVisualMapHeight;
            }
        }

        public int TrueMapWidth
        {
            get
            {
                return iTrueMapWidth;
            }
        }

        public int TrueMapHeight
        {
            get
            {
                return iTrueMapHeight;
            }
        }

        //Methods
        public static void LoadMap(string _sMapName)
        {
            string[] sMapRawLines = System.IO.File.ReadAllLines("../../" + _sMapName);
            string[][] sMapJagged = new string[sMapRawLines.Count()][];

            int iMapReadIncrementer = 0;

            foreach (string s in sMapRawLines)
            {
                sMapJagged[iMapReadIncrementer] = s.Split(' ');
                iMapReadIncrementer++;
            }
            //iMapWidth = (sMapJagged[0].Count() * 2) + 1;
            //iMapHeight = sMapRawLines.Count();

            mapMaster = new Map(sMapJagged);
            mapCurrent = new Map(sMapJagged);
        }

        public static int[] LocateStartingPoint()
        {
            int[] iPlayerStartCoords = { 0, 0 };

            for (int iRow = 0; iRow < mapMaster.TrueMapHeight; iRow++)
            {

                for (int iCol = 0; iCol < mapMaster.TrueMapWidth; iCol++)
                {
                    MapTile temp = mapMaster.mtMapData[iRow][iCol];

                    if (temp.TileSymbol == "@")
                    {
                        iPlayerStartCoords[0] = iRow;
                        iPlayerStartCoords[1] = iCol;
                        break;
                    }
                }
                if (0 != iPlayerStartCoords[0])
                {
                    break;
                }
            }

            return iPlayerStartCoords;
        }

        public static void PrintMap(Map _mapCurrent)
        {
            //Map with left and right borders
            for (int i = 0; i < _mapCurrent.mtMapData.Length; i++)
            {
                //Console.Write(UI.sSideBarLeft);
                Console.Write("|");
                Console.Write(" ");

                for (int x = 0; x < _mapCurrent.mtMapData[i].Count(); x++)
                {
                    Console.ForegroundColor = _mapCurrent.mtMapData[i][x].TileForeGroundColor;
                    Console.BackgroundColor = _mapCurrent.mtMapData[i][x].TileBackGroundColor;
                    Console.Write(_mapCurrent.mtMapData[i][x].TileSymbol);
                    Console.ResetColor();
                    Console.Write(" ");
                }

                //Console.Write(UI.sSideBarRight);
                Console.Write("|\n");
            }
        }



        ////public static int CheckMapTile(string sSymbol)
        ////{
        ////    //0 = Passable, non-interactable
        ////    //1 = Passable, interactable
        ////    //2 = Impassable, non-interactable
        ////    //3 = Impassable, interactable
        ////    switch (sSymbol)
        ////    {
        ////        case "#":
        ////            UI.sMsg1 = UI.sWall;
        ////            return 3;
        ////        case "^":
        ////            UI.sMsg1 = UI.sMountain;
        ////            return 2;
        ////        case "~":
        ////            UI.sMsg1 = UI.sWater;
        ////            return 2;
        ////        case "*":
        ////            UI.sMsg1 = UI.sStone;
        ////            return 1;
        ////        default:
        ////            UI.sMsg1 = UI.sDefaultMoving;
        ////            return 0;
        ////    }
        ////}

        //public static int[] LocateStartingPoint()
        //{
        //    int[] iPlayerStartCoords = { 0, 0 };
        //    int iStartRow = 0;
        //    int iStartCol = 0;

        //    for (int i = 0; i < sMapTemp.Length; i++)
        //    {

        //        if (-1 != Array.IndexOf(sMapTemp[i], "@"))
        //        {
        //            iStartRow = i;
        //            iStartCol = Array.IndexOf(sMapTemp[i], "@");
        //            break;
        //        }
        //    }

        //    iPlayerStartCoords[0] = iStartRow;
        //    iPlayerStartCoords[1] = iStartCol;

        //    return iPlayerStartCoords;
        //}

    }
}
