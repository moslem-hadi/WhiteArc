using System.Web.UI.WebControls;

namespace DNT.Extensions
{
    public static class DataExtensions
    {
        /// <summary>
        /// Bind to a ListControl (Dropdownlist, listbox, checkboxlist, radiobutton) in minimal amounts of code.
        ///  Also returns true false if items are in the control after  binding and sets the selected index to first value.
        /// Example:
        /// DropDownList ddl = new DropDownList();
        /// ddl.DataBind(data, "value", "key");
        /// </summary>
        /// <param name="control"></param>
        /// <param name="datasource"></param>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <returns></returns>
        public static bool DataBind(this ListControl control, object datasource, string textField, string valueField)
        {
            return DataBind(control, datasource, textField, null, valueField);
        }

        /// <summary>
        /// Bind to a ListControl (Dropdownlist, listbox, checkboxlist, radiobutton) in minimal amounts of code.
        ///  Also returns true false if items are in the control after  binding and sets the selected index to first value.
        /// Example:
        /// DropDownList ddl = new DropDownList();
        /// ddl.DataBind(data, "value", "key");
        /// </summary>
        /// <param name="control"></param>
        /// <param name="datasource"></param>
        /// <param name="textField"></param>
        /// <param name="textFieldFormat"> </param>
        /// <param name="valueField"></param>
        /// <returns></returns>
        public static bool DataBind(this ListControl control, object datasource, string textField, string textFieldFormat, string valueField)
        {
            control.DataTextField = textField;
            control.DataValueField = valueField;

            if (!string.IsNullOrEmpty(textFieldFormat))
                control.DataTextFormatString = textFieldFormat;

            control.DataSource = datasource;
            control.DataBind();

            if (control.Items.Count > 0)
            {
                control.SelectedIndex = 0;
                return true;
            }
            return false;
        }
    }
}
