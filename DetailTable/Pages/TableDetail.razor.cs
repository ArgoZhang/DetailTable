using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DetailTable.Pages
{
    public partial class TableDetail
    {
        [Parameter]
        public string PO_NO { get; set; }

        [Parameter]
        public string PO_Item { get; set; }

        private List<DetailVM> DetailItems { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            DetailItems = GenerateDetail();
        }

        private List<DetailVM> GenerateDetail()
        {
            List<DetailVM> list = new List<DetailVM>();
            for (int i = 0; i < 100; i+=2)
            {
                DetailVM dVM = new DetailVM()
                {
                    PO_NO = "NO" + i.ToString(),
                    PO_Item = "Item" + i.ToString(),
                    LotNo = "Lot"+i.ToString(),
                    DateCode = "2021"+i.ToString(),
                    Enabled = "Y"
                };
                list.Add(dVM);
            }
            return list;
        }

        protected Task<QueryData<DetailVM>> DetailQueryAsync(QueryPageOptions options)
        {
            IEnumerable<DetailVM> items = DetailItems.Where(x => x.PO_NO == PO_NO && x.PO_Item == PO_Item);


            // 设置记录总数
            var total = items.Count();

            // 内存分页
            items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();


            return Task.FromResult(new QueryData<DetailVM>
            {
                IsFiltered = true,
                IsSearch = true,
                IsSorted = true,
                TotalCount = total,
                Items = items
            });
        }
    }
}
