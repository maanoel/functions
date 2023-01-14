#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{           
        string responseMessage = await new StreamReader(req.Body).ReadToEndAsync();   
        dynamic data = JsonConvert.DeserializeObject(responseMessage);

        foreach(var reading in data.readings){
            log.LogInformation("Olá mundo");
            
            if(reading.temperature<=25) {
                reading.status = "OK";
            } else if (reading.temperature<=50) {
                reading.status = "CAUTION";
            } else {
                reading.status = "DANGER";
            }
        }
        
        return new OkObjectResult(data);
}
