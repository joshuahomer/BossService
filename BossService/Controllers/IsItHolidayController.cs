﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BossService.Models;
using System.Text.RegularExpressions;

namespace BossService.Controllers
{
    enum Holidays { XMAS, HWEEN, VALDAY, NEWYEARS, JULYFOUR };
    public class IsItHolidayController : ApiController
    {
        // GET: api/IsItHoliday
        public GenericRestResponse Get()
        {
            try
            {
                var returnList = new List<string>();
                var values = Enum.GetValues(typeof(Holidays));

                foreach (var val in values)
                {
                    var s = "";
                    if (CheckIfHoliday((Holidays)val))
                    {
                        s = "It is currently " + val + "!!!";
                    }
                    else
                    {
                        s = "It is not currently " + val;
                    }
                    returnList.Add(s);
                }
                return new GenericRestResponse
                {
                    Success = true,
                    Response = returnList
                };
            }
            catch(Exception e)
            {
                return new GenericRestResponse
                {
                    Success = false,
                    Response = new GenericRestError
                    {
                        Exception = e,
                        HumanMessage = "Something went wrong...Please try again."
                    }
                };
            }
        }

        // GET: api/IsItHoliday/5
        public GenericRestResponse Get(int id)
        {
            try
            {
                bool succ = CheckIfHoliday((Holidays)id);
                var s = "";

                if (succ)
                {
                    s = "It is currently " + (Holidays)id + "!!!";
                }
                else
                {
                    s = "It is not currently " + (Holidays)id;
                }
                return new GenericRestResponse
                {
                    Success = true,
                    Response = s
                };
            }
            catch(Exception e)
            {
                return new GenericRestResponse
                {
                    Success = false,
                    Response = new GenericRestError
                    {
                        Exception = e,
                        HumanMessage = "Something went wrong...Please try again."
                    }
                };
            }
        }

        // POST: api/IsItHoliday
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/IsItHoliday/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/IsItHoliday/5
        public void Delete(int id)
        {
        }

        private static bool CheckIfHoliday(Holidays holiday)
        {
            Regex r = new Regex("a");
            string p;

            switch (holiday)
            {
                case Holidays.XMAS:
                    p = @"12/25/\d\d\d\d .*";
                    r = new Regex(p);
                    break;
                case Holidays.HWEEN:
                    p = @"10/31/\d\d\d\d .*";
                    r = new Regex(p);
                    break;
                case Holidays.VALDAY:
                    p = @"2/14/\d\d\d\d .*";
                    r = new Regex(p);
                    break;
                case Holidays.NEWYEARS:
                    p = @"1/1/\d\d\d\d .*";
                    r = new Regex(p);
                    break;
                case Holidays.JULYFOUR:
                    p = @"7/4/\d\d\d\d .*";
                    r = new Regex(p);
                    break;
            }
            if(r.IsMatch("a"))
            {
                throw new Exception("Please pass in a valid Holiday Id. 0-4 are currently supported.");
            }
            if (r.IsMatch(DateTime.Now.ToString()))
            {
                return true;
            }
            return false;
        }
    }
}
