using System.Threading.Tasks;
using AspNetCore.ControllersAsFilters.Sample.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCore.ControllersAsFilters.Sample.Controllers
{
    public class ContactExistsAttribute : TypeFilterAttribute
    {
        public ContactExistsAttribute() : base(typeof(ContactExistsFilter))
        {
        }

        private class ContactExistsFilter : IAsyncActionFilter
        {
            private readonly IContactRepository _contactRepository;
            public ContactExistsFilter(IContactRepository contactRepository)
            {
                _contactRepository = contactRepository;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("id"))
                {
                    var id = (int)context.ActionArguments["id"];
                    if (await _contactRepository.Get(id) == null)
                    {
                        context.Result = new NotFoundResult();
                        return;
                    }
                }

                await next();
            }
        }
    }
}
