using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
public interface IFormBuilder
{
    public void AddInputTextFormGroup(string Name, string Title, string Value, Action<string> OnChange);
    public void AddCheckboxFormGroup(string Name, string Title, bool Value, Action<string> OnChange);
    public void AddSelectBoxFormGroup(string Name, string Title, string Value, IEnumerable<string> options, Action<string> OnChange);
    public void AddSelectBoxFormGroup(string Name, string Title, int Value, IDictionary<int, string> options, Action<int, string> OnChange);
}
public class WebFormBuilderService: IFormBuilder
{
    private WebForm Target = new WebForm();
    public class BaseContentPane: BaseElement        
    {
        protected ConcurrentBag<BaseElement> Children { get; set; }
            = new ConcurrentBag<BaseElement>();

        public void Append(BaseElement Element)        
            => this.Children.Add(Element);
        
    }
    public class BaseElement
    {
        public string Name { get; set; }
        public string Title { get; set; }

    }

    public class BaseControl : BaseContentPane
    {
        public string Value { get; set; }
        public Action<string> OnChange { get; set; }
    }
    public class InputTextControl : BaseControl
    {
        
    }
    public class InputCheckControl : BaseControl
    {

    }
    public class InputSelectControl : BaseControl
    {
        public IDictionary<int, string> Options { get; internal set; }
    }
    public class InputTextboxControl : BaseControl
    {

    }
    public class WebForm : BaseContentPane
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string URL { get; set; }
        public IEnumerable<BaseElement> GetControls()
            => this.Children;

    }
    public WebFormBuilderService()
    {
    }

    public void AddTitle(string Title)
        => this.Target.Title = Title;


    public WebForm Build()
    {        
        return Target;
    }

   

    public void AddInputTextFormGroup(string Name, string Title, string Value, Action<string> OnChange)
    {
        this.Target.Append(new InputTextControl()
        {
            Name = Name,
            Title = Title,
            Value = Value,
            OnChange = OnChange
        });
    }

    public void AddCheckboxFormGroup(string Name, string Title, bool Value, Action<string> OnChange)
    {
        this.Target.Append(new InputCheckControl()
        {
            Name = Name,
            Title = Title,
            Value = Value.ToString(),
            OnChange = OnChange
        });
    }

    public void AddSelectBoxFormGroup(string Name, string Title, string Value, IEnumerable<string> Options, Action<string> OnChange)
    {


        Dictionary<int,string> OptionsMap = new Dictionary<int, string>();
        foreach(string Option in Options)
        {
            OptionsMap[OptionsMap.Count] = Option;
        }
        this.Target.Append(new InputSelectControl()
        {
            Name = Name,
            Title = Title,
            Value = Value.ToString(),
            Options = OptionsMap,
            OnChange = OnChange
        });
    }

    public void AddSelectBoxFormGroup(string Name, string Title, int Value, IDictionary<int, string> Options, Action<string> OnChange)
    {
        this.Target.Append(new InputSelectControl()
        {
            Name = Name,            
            Title = Title,
            Value = Value.ToString(),
            Options = Options,
            OnChange = OnChange
        });
    }

    public void AddSelectBoxFormGroup(string Name, string Title, int Value, IDictionary<int, string> options, Action<int, string> OnChange)
    {
        throw new NotImplementedException();
    }
}