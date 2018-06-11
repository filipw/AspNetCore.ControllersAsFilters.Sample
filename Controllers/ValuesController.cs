using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.ControllersAsFilters.Sample.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCore.ControllersAsFilters.Sample.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase, IAsyncActionFilter
    {
        private readonly IContactRepository _repository;

        public ContactsController(IContactRepository repository)
        {
            _repository = repository ?? throw new System.ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> Get()
        {
            return Ok(await _repository.GetAll());
        }

        [HttpGet("{id}", Name = "GetContactById")]
        public async Task<ActionResult<Contact>> Get(int id)
        {
            var contact = await _repository.Get(id);
            return Ok(contact);
        }

        [NonAction]
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments.ContainsKey("id"))
            {
                var id = (int)context.ActionArguments["id"];
                if (await _repository.Get(id) == null)
                {
                    context.Result = new NotFoundResult();
                    return;
                }
            }

            await next();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Contact contact)
        {
            var newId = await _repository.Add(contact);
            return CreatedAtRoute("GetContactById", new { id = newId }, contact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Contact contact)
        {
            contact.ContactId = id;
            await _repository.Update(contact);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.Delete(id);
            return NoContent();
        }
    }
}
