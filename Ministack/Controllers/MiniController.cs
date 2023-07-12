using Microsoft.AspNetCore.Mvc;
using Ministack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;


namespace Ministack.Controllers
{
    public class MiniController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [Route("AddOrUpdateSchedule")]
        [HttpPost]
        public Response AddOrUpdateSchedule(Schedule st)
        {
            string message = "";
            try
            {
                using (var db = new MiniContext())
                {
                    if (st.Id == 0)
                    {
                        db.Add(st);
                        db.SaveChanges();

                        message = "Added Successfully";
                    }
                    else
                    {
                        Schedule schedule = db.Schedule.FirstOrDefault(b => b.Id == st.Id);
                        if (schedule != null)
                        {
                            schedule.Name = st.Name;
                            schedule.Description = st.Description;
                            db.SaveChanges();

                            message = "Updated Successfully";
                        }

                    }
                }

                return new Response
                {
                    Status = "Success",
                    Message = message
                };
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return new Response
            {
                Status = "Error",
                Message = "Data not insert"
            };
        }

        [Route("GetSchedules")]
        [HttpGet]
        public IEnumerable<Schedule> GetSchedules()
        {
            var schedules = new List<Schedule>();
            try
            {
                using (var db = new MiniContext())
                {
                    schedules = db.Schedule.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return schedules;
        }

        [Route("GetScheduleById")]
        [HttpGet]
        public Schedule GetSchedule(int id)
        {
            try
            {
                Schedule schedule = null;

                using (var db = new MiniContext())
                {
                    schedule = db.Schedule.FirstOrDefault(b => b.Id == id);
                }

                return schedule;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return null;
        }

        [Route("DeleteSchedule")]
        [HttpDelete]
        public Response DeleteSchedule(int id)
        {
            try
            {
                using (var db = new MiniContext())
                {
                    Schedule schedule1 = GetSchedule(id);
                    if (schedule1 != null)
                    {
                        db.Remove(schedule1);
                        db.SaveChanges();
                    }
                }

                return new Response
                {
                    Status = "Success",
                    Message = "Deleted Successfully"
                };
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return new Response
            {
                Status = "Error",
                Message = "Data not deleted"
            };
        }

    }
}