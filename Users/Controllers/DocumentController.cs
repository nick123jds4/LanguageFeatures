using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Users.Models;

namespace Users.Controllers
{
    [Authorize]
    public class DocumentController : Controller
    {
        private ProtectedDocument[] _documents = new ProtectedDocument[] {
        new ProtectedDocument{ Title = "Q3 Budget", Author="Alice", Editor="Joe"},
        new ProtectedDocument{ Title = "Project Plan", Author="Bob", Editor="Alice"}, 
        };
        private readonly IAuthorizationService _authorizationService;

        public DocumentController(IAuthorizationService authorizationService) => _authorizationService = authorizationService;

        public IActionResult Index()
        {
            return View(_documents);
        }

        public async Task<IActionResult> Edit(string title) {
            var document = _documents.FirstOrDefault(d=>d.Title == title);
            var result = await _authorizationService.AuthorizeAsync(User, document, "AuthorsAndEditors");
            if (result.Succeeded)
            {
                return View(nameof(Index), document);
            }
            else {
                //сообщить инфраструктуре mvc что произошел отказ в авторизации
                return new ChallengeResult();
            }
        }
    }
}
