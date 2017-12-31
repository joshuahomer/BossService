using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BossService.Models;

namespace BossService.Controllers
{
    public class RandomSentencesController : ApiController
    {
        // GET: api/RandomSentences
        public GenericRestResponse Get()
        {
            try
            {
                Random r = new Random();
                var returnString = "";
                var returnTitle = "";
                int rInt = 0;
                int tInt = r.Next(1, 3);
                string line;
                var done = false;
                var startedWriting = false;
                var count = 0;
                var pCount = 1;
                //var test = BossService.Resources.Fellowship;

                int hInt = r.Next(1, 4);
                string startupPath = Environment.CurrentDirectory;
                System.IO.StreamReader file = file = new System.IO.StreamReader(startupPath + @"\LOTRFellowShip.txt");
                switch (hInt)
                {
                    case 1:
                        rInt = r.Next(0, 4129);
                        returnTitle = "The Fellowship of the Ring";
                        break;
                    case 2:
                        rInt = r.Next(0, 3170);
                        returnTitle = "The Two Towers";
                        file = new System.IO.StreamReader(startupPath + @"\LOTRTwoTowers.txt");
                        break;
                    case 3:
                        rInt = r.Next(0, 2558);
                        returnTitle = "The Return of the King";
                        file = new System.IO.StreamReader(startupPath + @"\LOTRROTK.txt");
                        break;
                }
       
                while ((line = file.ReadLine()) != null)
                {
                    if(startedWriting)
                    {
                        var charArr = line.ToCharArray();

                        for (int idx = 0; idx < charArr.Length; idx++)
                        {
                            if (charArr[idx] == '.')
                            {
                                pCount++;
                                if (pCount <= tInt)
                                {
                                    returnString += charArr[idx];
                                }
                                else
                                {
                                    done = true;
                                    returnString += charArr[idx];
                                    break;
                                }
                            }
                            else
                            {
                                returnString += charArr[idx];
                            }
                        }
                    }
                    if (count == rInt)
                    {
                        if(line != "")
                        {
                            startedWriting = true;
                            var charArr = line.ToCharArray();
                            var validStart = false;

                            for(int idx=0;idx<charArr.Length;idx++)
                            {
                                if(!validStart && (char.IsUpper(charArr[idx]) || charArr[idx] == '.'))
                                {
                                    validStart = true;
                                }
                                if(validStart)
                                {
                                    if (charArr[idx] == '.' && idx >= 2 &&(charArr[idx-2] != 'M' && charArr[idx - 1] != 'r'))
                                    {
                                        pCount++;
                                        if (pCount <= tInt)
                                        {
                                            returnString += charArr[idx];
                                        }
                                        else
                                        {
                                            done = true;
                                            returnString += charArr[idx];
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        returnString += charArr[idx];
                                    }
                                }
                            }
                        }
                        else
                        {
                            rInt++;
                        }
                    }
                    count++;
                    if(done)
                    {
                        break;
                    }
                }
                return new GenericRestResponse
                {
                    Success = true,
                    Response = new RandomSentenceReply
                    {
                        BookTitle = returnTitle,
                        Sentences = returnString
                    }
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

        // GET: api/RandomSentences/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/RandomSentences
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/RandomSentences/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/RandomSentences/5
        public void Delete(int id)
        {
        }
    }
}
