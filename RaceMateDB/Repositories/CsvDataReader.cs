using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RaceMateDB.Models;
using System.IO;

namespace RaceMateDb.Repositories
{
    public class CsvDataReader
    {

        public RMDBContext db = new RMDBContext();

        public void ReadCourseCsv(HttpPostedFileBase file, List<CourseModel> importedCourseModelList)
        {
            if (file.ContentLength > 0)
            {
                var column1 = new List<string>();
                var column2 = new List<string>();

                using (var rdr = new StreamReader(file.InputStream))
                {
                    while (!rdr.EndOfStream)
                    {
                        var splits = rdr.ReadLine().Split(',');

                        if (!String.IsNullOrEmpty(splits[0]))
                        {

                            var newCourse = new CourseModel();

                            newCourse.Name = splits[0];
                            newCourse.Description = splits[1];
                            newCourse.VeloViewerURL = splits[2];
                            importedCourseModelList.Add(newCourse);

                            Console.WriteLine(newCourse.Name + "added to model");
                        }
                    }
                    Console.WriteLine("Done");
                }
            }
        }
        
        public void ReadEventCSV(HttpPostedFileBase file, List<EventModel> importedEventModelList)
        {


            if (file.ContentLength > 0)
            {


                var column1 = new List<string>();
                var column2 = new List<string>();

                using (var rdr = new StreamReader(file.InputStream))
                {
                    //   while (!rdr.EndOfStream)
                    rdr.ReadLine();                     //Peeks first line and ignores it.
                    while (rdr.Peek() != -1)
                    {
                        var splits = rdr.ReadLine().Split(',');

                        if (!String.IsNullOrEmpty(splits[0]))
                        {
                            var newEvent = new EventModel();

                            newEvent.Name = splits[0];

                            // GET COURSEIDBYNAMEMETHOD                                                             
                            string searchTerm = splits[1]; // Array reference will not run inside of query

                            var eventCourseModel = db.CourseModels
                                               .Where(r => r.Name.Contains(searchTerm));  //Need some whitespace handling!!!                                                                                              

                            newEvent.CourseId = eventCourseModel.FirstOrDefault().Id;                           //Sets up objects         
                            newEvent.CourseName = eventCourseModel.FirstOrDefault().Name;
                            newEvent.Date = Convert.ToDateTime(splits[2]);

                            importedEventModelList.Add(newEvent);

                            Console.WriteLine(newEvent.Name + " " + newEvent.Date.ToString() + " added to model");
                        }

                    }
                    Console.WriteLine("Done");

                }


            }
        }

        public void ReadResultCsv(HttpPostedFileBase file, List<ResultModel> importedResultModelList)
        {
            if (file.ContentLength > 0)
            {
                var column1 = new List<string>();
                var column2 = new List<string>();

                using (var rdr = new StreamReader(file.InputStream))
                {
                    //   while (!rdr.EndOfStream)
                    string eventName = rdr.ReadLine();    //Get Event details initialized 
                    var eventModel = db.EventModels
                                                   .Where(r => r.Name.Contains(eventName));  //Need some whitespace handling!!!   


                    //Peeks first line and ignores it.
                    while (rdr.Peek() != -1)
                    {
                        var splits = rdr.ReadLine().Split(',');

                        if (!String.IsNullOrEmpty(splits[0]))
                        {
                            var newResult = new ResultModel();


                            // GET OBJECT                                                        

                            string riderName = splits[1];

                            var riderModel = db.RiderModels
                                                          .Where(r => r.Name.Contains(riderName));  //Need some whitespace handling!!!   
                            Console.WriteLine(riderModel.FirstOrDefault().Name);

                            newResult.EventName = eventModel.FirstOrDefault().Name;
                            newResult.EventModelId = eventModel.FirstOrDefault().Id;
                            newResult.RiderModelId = riderModel.FirstOrDefault().Id;
                            newResult.RiderName = riderModel.FirstOrDefault().Name;
                            int positionInteger = Convert.ToInt32(splits[0]);
                            newResult.Position = positionInteger;                    //Sets up objects         




                            importedResultModelList.Add(newResult);


                        }

                    }
                    Console.WriteLine("Done");

                }


            }
        }
    }
}