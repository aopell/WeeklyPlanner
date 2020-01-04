using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WeeklyPlanner.Items
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ChecklistItem : CheckBox
    {
        private TextFormatting textFormatting;

        [JsonProperty]
        public string Title { get; set; }
        [JsonProperty]
        public bool MainList { get; set; }
        [JsonProperty]
        public List<string> SubParts { get; set; }
        [JsonProperty]
        public TextFormatting TextFormatting
        {
            get => textFormatting;
            set
            {
                textFormatting = value;
                FontWeight = (value & TextFormatting.Bold) > 0 ? FontWeights.Bold : FontWeights.Regular;
                FontStyle = (value & TextFormatting.Italics) > 0 ? FontStyles.Italic : FontStyles.Normal;
            }
        }

        public ChecklistItem()
        {
            VerticalContentAlignment = VerticalAlignment.Center;
        }

        //static ChecklistItem()
        //{
        //    ContentProperty.OverrideMetadata(typeof(CheckBox), new PropertyMetadata(null, null, (c, v) =>
        //    {
        //        var item = c as ChecklistItem;
        //        var builder = new StringBuilder();
        //        builder.Append(item.Title);
        //        foreach (string s in item.SubParts)
        //        {
        //            builder.AppendFormat(" ({0})", s);
        //        }
        //        return builder.ToString();
        //    }));
        //}
    }
}
