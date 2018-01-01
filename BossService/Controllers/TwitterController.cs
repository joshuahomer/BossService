using System.Collections.Generic;
using System.Web.Http;
using BossService.Models;
using TweetSharp;
using System;

namespace BossService.Controllers
{
    //http://www.itprotoday.com/software-development/how-use-tweetsharp-push-twitter-updates-application
    public class TwitterController : ApiController
    {
        public string consumerToken = "YpNrcREM06ucFJzGB8jWeQXyj";
        public string consumerSecret = "IFfTI3IQH4T8yHpmiMr41s7sFxDSROqw7LbDpWEA4HsTRPGxBn";
        public string accessToken = "947874013639974913-xwhPlnTfMZ3Deg2vRcXtvqfnTCDcGEu";
        public string accessTokenSecret = "cilNrUAjUEIYKVq5adJZI1y3rLZPpTVOU65m3O5mhcoQi";

        // GET: api/Twitter
        public GenericRestResponse Get()
        {
            return new GenericRestResponse
            {
                Success = false,
                Response = "This endpoint is currently unsupported."
            };
        }

        // GET: api/Twitter/5
        public GenericRestResponse Get(int id)
        {
            try
            {
                if (id != 245687543)
                {
                    throw new Exception("wrong id, i dont know who you are.");
                }
                var ransengen = new RandomSentencesController();
                var tuple = ransengen.RandomSentence();
                var service = new TwitterService(consumerToken, consumerSecret);
                service.AuthenticateWith(accessToken, accessTokenSecret);
                var options = new SendTweetOptions
                {
                    Status = tuple.Sentences + " (" + tuple.Title + ")",
                };
                var result = service.SendTweet(options);
                if (result == null)
                {
                    throw new Exception("Something went wrong when sending the tweet");
                }
                return new GenericRestResponse
                {
                    Success = true,
                    Response = result
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

        // POST: api/Twitter
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Twitter/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Twitter/5
        public void Delete(int id)
        {
        }
    }
}
