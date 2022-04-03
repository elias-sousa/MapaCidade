using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MapaCidade.Dominio.Core.Entities;
using MapaCidade.Infra.Bd.Config;
using MapaCidade.Aplicacao.Interface;
using MapaCidade.Apresentacao.MVC.Models;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace MapaCidade.Apresentacao.MVC.Controllers
{
    public class EstacaoRecargaController : Controller
    {
        private readonly IEstacaoRecargaService _estacaoRecargaService;

        private readonly IStringLocalizer<EstacaoRecargaController> _localizer;
        private readonly IHostingEnvironment _hostingEnvironment;

        public EstacaoRecargaController(IEstacaoRecargaService estacaoRecargaService,
            IStringLocalizer<EstacaoRecargaController> localizer, IHostingEnvironment hostingEnvironment)
        {
            _localizer = localizer;
            _estacaoRecargaService = estacaoRecargaService;
            _hostingEnvironment = hostingEnvironment;
        }

        public ActionResult Index(string hdLatitude, string hdLongitude)
        {

            if (!String.IsNullOrEmpty(hdLatitude))
            {
                return View(_estacaoRecargaService.ProcurarEstacoesProximas(
                    Convert.ToDouble(hdLatitude),
                    Convert.ToDouble(hdLongitude)));
            }
            else
            {
                return View(_estacaoRecargaService.ListarTodos());
            }
        }

        public ActionResult Create()
        {
            var model = new EstacaoRecargaModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EstacaoRecargaModel model)
        {
            try
            {
                EstacaoRecarga estacaoRecarga = new EstacaoRecarga();
                estacaoRecarga.Id = Guid.NewGuid();
                estacaoRecarga.Nome = model.Nome;
                estacaoRecarga.Tipo = model.Tipo;
                estacaoRecarga.Latitude = model.Latitude;
                estacaoRecarga.Longitude = model.Longitude;

                _estacaoRecargaService.Adicionar(estacaoRecarga);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.JavaScriptFunction = ex.Message;
                return View(model);
            }
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estacaoRecarga = _estacaoRecargaService.EncontrarPorId(id.Value);

            if (estacaoRecarga == null)
            {
                return NotFound();
            }

            return View(estacaoRecarga);
        }

        // GET: EstacaoRecargas/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estacaoRecarga = _estacaoRecargaService.EncontrarPorId(id.Value);

            if (estacaoRecarga == null)
            {
                return NotFound();
            }

            EstacaoRecargaModel model = new EstacaoRecargaModel();
            model.Id = estacaoRecarga.Id;
            model.Latitude = estacaoRecarga.Latitude;
            model.Longitude = estacaoRecarga.Longitude;
            model.Nome = estacaoRecarga.Nome;
            model.Tipo = estacaoRecarga.Tipo;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, EstacaoRecargaModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    EstacaoRecarga estacaoRecarga = new EstacaoRecarga();
                    estacaoRecarga.Id = model.Id;
                    estacaoRecarga.Nome = model.Nome;
                    estacaoRecarga.Tipo = model.Tipo;
                    estacaoRecarga.Latitude = model.Latitude;
                    estacaoRecarga.Longitude = model.Longitude;
                    _estacaoRecargaService.Atualizar(estacaoRecarga);
                }
                catch (Exception ex)
                {
                    ViewBag.JavaScriptFunction = ex.Message;
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: EstacaoRecargas/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estacaoRecarga = _estacaoRecargaService.EncontrarPorId(id.Value);

            if (estacaoRecarga == null)
            {
                return NotFound();
            }

            return View(estacaoRecarga);
        }

        // POST: EstacaoRecargas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var estacaoRecarga = _estacaoRecargaService.Deletar
                (_estacaoRecargaService.EncontrarPorId(id));
            return RedirectToAction(nameof(Index));
        }

       
        public FileResult Download(Guid id)
        {
            var path = _estacaoRecargaService.GerarArquivo(id, Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot"));

            FileStream fileStream;

            try
            {
                fileStream = System.IO.File.OpenRead(path);

                return File(fileStream, GetContentType(path), fileStream.Name);
            }
            catch (DirectoryNotFoundException)
            {
                return null;
            }
            finally
            {
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
            }           
                       
        }

        public string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            string ret;

            try
            {
                ret = types[ext];
            }
            catch (Exception)
            {
                ret = "application/octet-stream";
            }
            return ret;
        }

        private static Dictionary<string, string> GetMimeTypes()
        {

            return new Dictionary<string, string>
            {
                {".aac","audio/aac"},
                {".abw","application/x-abiword"},
                {".arc","application/octet-stream"},
                {".avi","video/x-msvideo"},
                {".azw","application/vnd.amazon.ebook"},
                {".bin","application/octet-stream"},
                {".bmp", "image/bmp"},
                {".bz","application/x-bzip"},
                {".bz2","application/x-bzip2"},
                {".csh","application/x-csh"},
                {".css","text/css"},
                {".csv","text/csv"},
                {".doc","application/msword"},
                {".docx", "application/vnd.ms-word"},
                {".eot","application/vnd.ms-fontobject"},
                {".epub","application/epub+zip"},
                {".gif","image/gif"},
                {".htm","text/html"},
                {".html","text/html"},
                {".ico","image/x-icon"},
                {".ics","text/calendar"},
                {".jar","application/java-archive"},
                {".jpeg","image/jpeg"},
                {".jpg","image/jpeg"},
                {".js","application/javascript"},
                {".json","application/json"},
                {".mid","audio/midi"},
                {".midi","audio/midi"},
                {".mpeg","video/mpeg"},
                {".mpkg","application/vnd.apple.installer+xml"},
                {".odp","application/vnd.oasis.opendocument.presentation"},
                {".ods","application/vnd.oasis.opendocument.spreadsheet"},
                {".odt","application/vnd.oasis.opendocument.text"},
                {".oga","audio/ogg"},
                {".ogv","video/ogg"},
                {".ogx","application/ogg"},
                {".otf","font/otf"},
                {".png","image/png"},
                {".pdf","application/pdf"},
                {".ppt","application/vnd.ms-powerpoint"},
                {".rar","application/x-rar-compressed"},
                {".rtf","application/rtf"},
                {".sh","application/x-sh"},
                {".svg","image/svg+xml"},
                {".swf","application/x-shockwave-flash"},
                {".tar","application/x-tar"},
                {".tif","image/tiff"},
                {".tiff","image/tiff"},
                {".ts","application/typescript"},
                {".ttf","font/ttf"},
                {".txt", "text/plain"},
                {".vsd","application/vnd.visio"},
                {".wav","audio/x-wav"},
                {".weba","audio/webm"},
                {".webm","video/webm"},
                {".webp","image/webp"},
                {".woff","font/woff"},
                {".woff2","font/woff2"},
                {".xhtml","application/xhtml+xml"},
                {".xls","application/vnd.ms-excel"},
                {".xlsx","application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".xml","application/xml"},
                {".xul","application/vnd.mozilla.xul+xml"},
                {".zip","application/zip"},
                {".3gp","video/3gpp"},
                {".3g2","video/3gpp2"},
                {".7z","application/x-7z-compressed"}
            };
        }

    }
}
