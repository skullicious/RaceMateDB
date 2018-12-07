using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RaceMateDB.Models;
using System.IO;
using RaceMateDB.ViewModels;

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

        public static PredictedResultViewModel BuildPredictedResult(ResultModel item)
        {
            //build each riders predicted result
            var model = new PredictedResultViewModel();

            model.RiderID = item.RiderModelId;
            model.RiderName = item.Rider.Name;
            model.NumberofResults = item.Rider.EventResults.Count();

            //calculate weighting of points
            model.ResultWeighting += PredictedResultLogic.CreateResultWeightingFromPosition(item.Position);

            //divide weighted points by number of races
            model.ResultWeighting = model.ResultWeighting / model.NumberofResults;
            return model;
        }

        public static void CheckNumberOfResultsAndDiscardExisting(List<PredictedResultViewModel> modelList, PredictedResultViewModel model, int i)
        {
            var existingModel = modelList[i];

            if ((model.RiderID == existingModel.RiderID) && (existingModel.NumberofResults < model.NumberofResults))
            {
                //Add existing points to new object
                model.ResultWeighting += existingModel.ResultWeighting;
                //discard old object
                modelList.Remove(existingModel);
                Console.WriteLine("Removing model!");
            }
        }
        
        public static void GeneratePredictedResultModel(List<PredictedResultViewModel> modelList, ResultModel item)
        {
            PredictedResultViewModel model = PredictedResultLogic.BuildPredictedResult(item);

            //iterate through results and discard existing
            for (int i = 0; i < modelList.Count; i++)
            {
                PredictedResultLogic.CheckNumberOfResultsAndDiscardExisting(modelList, model, i);
            }
            // add riders predicted result to model
            modelList.Add(model);
        }
    }
}