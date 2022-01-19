using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DetailTable.Common
{
    public static class BootstrapBlazorUtil
    {
        public static readonly List<int> PageItemsSource = new() { 10, 20, 50 };

        #region -- Table --

        public static void DefaultSetup<TItem>(this Table<TItem> table)
            where TItem : class, new()
        {
            foreach (var column in table.Columns)
            {
                column.Filterable = true;
                column.Sortable = true;
            }
        }

        public static Task<QueryData<TItem>> EmptyItem<TItem>(this Table<TItem> table)
             where TItem : class, new()
        {
            return Task.FromResult(new QueryData<TItem>
            {
                IsFiltered = true,
                IsSearch = true,
                IsSorted = true,
                TotalCount = 0,
                Items = Enumerable.Empty<TItem>()
            });
        }

        #endregion -- Table --

        #region -- 訊息元件 --

        public static readonly Dictionary<MessageCategory, MessageCategoryData> RefMessageCategory
            = new()
            {
                [MessageCategory.Information] = new MessageCategoryData
                {
                    Color = Color.Info,
                    Icon = "fa fa-info-circle"
                },
                [MessageCategory.Warning] = new MessageCategoryData
                {
                    Color = Color.Warning,
                    Icon = "fa fa-exclamation-circle"
                },
                [MessageCategory.Error] = new MessageCategoryData
                {
                    Color = Color.Danger,
                    Icon = "fa fa-times-circle"
                }
            };

        public static void ShowMessage(Message messageElement, Placement placement, MessageService messageService, string content, Color color, string iconClass)
        {
            messageElement.SetPlacement(placement);
            messageService?.Show(new MessageOption()
            {
                Host = messageElement,
                Content = content,
                Color = color,
                Icon = iconClass,
                Delay = 2
            });
        }


        #endregion -- 訊息元件 --

        #region -- Radio --

        public static void Reset(this Radio control)
        {
            foreach (var item in control.Items)
            {
                item.Active = false;
            }
        }

        #endregion -- Radio --

        #region -- JSRuntimeExtensions --

        /// <summary>
        /// 调用 JSInvoke 方法
        /// </summary>
        /// <param name="jsRuntime">IJSRuntime 实例</param>
        /// <param name="el">Element 实例或者组件 Id</param>
        /// <param name="func">Javascript 方法</param>
        /// <param name="args">Javascript 参数</param>
        public static async ValueTask InvokeVoidAsync(this IJSRuntime jsRuntime, ElementReference? el = null, string func = null, params object[] args)
        {
            var paras = new List<object>();
            if (el != null) paras.Add(el);
            if (args != null) paras.AddRange(args);
            await jsRuntime.InvokeVoidAsync($"$.{func}", paras.ToArray());
        }

        #endregion -- JSRuntimeExtensions --

    }

    /// <summary>
    /// 訊息種類
    /// </summary>
    public enum MessageCategory
    {
        Information = 2,
        Warning = 3,
        Error = 4
    }

    /// <summary>
    /// enum MessageCategory 資訊
    /// </summary>
    public class MessageCategoryData
    {
        /// <summary>
        /// 按鈕樣式(font-awesome class)
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 顏色
        /// </summary>
        public Color Color { get; set; }
    }
}
