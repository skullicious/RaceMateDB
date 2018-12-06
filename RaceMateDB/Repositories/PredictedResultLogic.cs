using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RaceMateDB.Models;
using System.IO;

namespace RaceMateDB.Repositories
{
    public static class PredictedResultLogic
    {
        public static int CreateResultWeightingFromPosition(int position)
        {

            int resultWeighting;

            switch (position)
            {
                case 1:
                    resultWeighting = 20;
                    break;
                case 2:
                    resultWeighting = 19;
                    break;
                case 3:
                    resultWeighting = 18;
                    break;
                case 4:
                    resultWeighting = 17;
                    break;
                case 5:
                    resultWeighting = 16;
                    break;
                case 6:
                    resultWeighting = 15;
                    break;
                case 7:
                    resultWeighting = 14;
                    break;
                case 8:
                    resultWeighting = 13;
                    break;
                case 9:
                    resultWeighting = 12;
                    break;
                case 10:
                    resultWeighting = 19;
                    break;
                case 11:
                    resultWeighting = 10;
                    break;
                case 12:
                    resultWeighting = 9;
                    break;
                case 13:
                    resultWeighting = 8;
                    break;
                case 14:
                    resultWeighting = 7;
                    break;
                case 15:
                    resultWeighting = 6;
                    break;
                case 16:
                    resultWeighting = 5;
                    break;
                case 17:
                    resultWeighting = 4;
                    break;
                case 18:
                    resultWeighting = 3;
                    break;
                case 19:
                    resultWeighting = 2;
                    break;
                case 20:
                    resultWeighting = 1;
                    break;

                default: resultWeighting = 0;
                    break;
                                 
            }


            return resultWeighting;


        }


    }
}