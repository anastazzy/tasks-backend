using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Dto;

namespace WebApplication1.Controllers
{
    [Route("api/earth-picture")]
    [ApiController]
    public class EarthPictureController : Controller
    {
        private readonly HttpClient _client;

        public EarthPictureController()
        {
            _client = new HttpClient();
        }

        [HttpGet]
        public async Task<FileContentResult> Index([FromQuery]GetEarthPictureDto p)
        {
            var url = "https://api.nasa.gov/planetary/earth/imagery";
            var urlParams = $"?api_key=Usw5Wr7QN8DFeKVpREGZI43uxrAYfQ6FoXGOEF34&lon={p.Lon}&lat={p.Lat}&date={p.Date}&dim={p.Dim}";
            var response = await _client.GetAsync(url + urlParams);

            return File(await response.Content.ReadAsByteArrayAsync(), "image/png");
        }
    }
}
