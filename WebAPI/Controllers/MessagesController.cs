using Microsoft.Cognitive.LUIS;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class MessagesController : ApiController
    {
        [HttpPost]
        public virtual async Task<Command> GetCommand(string message)
        {
            var client = new LuisClient("{app_id}", "{app_key}");
            var result = await client.Predict(message);
            
            var name = result.TopScoringIntent.Name;
            var parameter = string.Empty;

            if (name.Equals("conversation.about"))
                parameter = "Olá! Estou aqui para fazer o que me pede. Deseja algo?";
            else
                parameter = result.GetAllEntities().FirstOrDefault()?.Value ?? string.Empty;

            return new Command(name, parameter);
        }
    }
}