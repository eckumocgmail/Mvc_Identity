using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using static WebFormBuilderService;

public class WebApplicationDbContext : IdentityDbContext
{


    public WebApplicationDbContext(DbContextOptions<WebApplicationDbContext> options)
        : base(options)
    {
    }

    protected WebApplicationDbContext()
    {
    }

    public IEnumerable<WebForm> GetActionForms()    
        => GetType().Assembly.GetTypes()
        .Where(t => t.Name.EndsWith("Controller"))
        .SelectMany(t => t.GetMethods()
            .Where(m => typeof(Controller).GetMethods().Select(cm => cm.Name).Contains(m.Name)==false)
            .Where(m=>m.Name.StartsWith("get_")==false && m.Name.StartsWith("set_")==false)
            .Select(m => 
            {
                var Form = CreateForm(t.Name, m.Name);
                Form.Controller = t.Name;
                Form.Action = m.Name;
                Form.URL = $"/{t.Name.Replace("Controller","")}/{m.Name}";
                return Form;
            }))       
        .ToList();
    


    public WebForm CreateForm(string controller, string action)
    {
        var formBuilder = new WebFormBuilderService();
        formBuilder.AddTitle($"{controller} {action}");
        var ctrl = GetType().Assembly.GetTypes()
            .Where(t => t.Name == controller).First();
        foreach(var param in ctrl.GetMethods().First(m => m.Name == action).GetParameters())
        {
            //model,property,value => message
  
            var validators = new List<Func<object, object, object, string>>(); //валидаторы
            switch (param.ParameterType.FullName)
            {
                case nameof(System.String):
                    formBuilder.AddInputTextFormGroup(param.Name, param.Name, param.Name, (value) => { });
                    break;
                case nameof(System.DateTime):
                    formBuilder.AddSelectBoxFormGroup(param.Name, "", "", null, (value) => { });
                    break;
                case nameof(System.Boolean):
                    formBuilder.AddCheckboxFormGroup(param.Name, param.Name, false, (value) => { });
                    break;
                default:
                    formBuilder.AddInputTextFormGroup(param.Name, param.Name, param.Name, (value) => { });
                    break;
            }
        }
        return formBuilder.Build();





    }

}
