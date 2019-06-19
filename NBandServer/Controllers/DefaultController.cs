using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NBandServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private NDbContext db;
        public DefaultController()
        {
            this.db = new NDbContext();
        }
        [HttpGet]
        public NormalResponse Test()
        {
            return new NormalResponse(true, "", "", "");
        }
        [HttpGet]
        public  NormalResponse GetFileMissions()
        {
            var list = db.FileMissionTable.ToList();
            return new NormalResponse(true, "", "", list);
        }
        [HttpPost]
        public NormalResponse UploadFileBlock(FileBlock file)
        {           
            if (file == null) return new NormalResponse(false, "文件块为空");
            return FileBlock.WritrBuffer(file);
        }
    }
}