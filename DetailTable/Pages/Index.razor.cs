using BootstrapBlazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DetailTable.Pages
{
    public partial class Index
    {
        private List<MasterVM> Items { get; set; }

        private List<DetailVM> DetailItems { get; set; }

        private SearchVM searchVM { get; set; } = new SearchVM();
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Items = GenerateMaster();
            DetailItems = GenerateDetail();
        }

        private List<MasterVM> GenerateMaster()
        {
            List<MasterVM> list = new List<MasterVM>();
            for(int i = 0; i < 20; i++)
            {
                MasterVM mVM = new MasterVM()
                {
                    PO_NO = "NO"+i.ToString(),
                    PO_Item = "Item"+i.ToString(),
                    Part_No = "Part_No"+i.ToString(),
                    Qty = i+10,
                    Recive_Qty = i+20,
                    Return_Qty = i+30,
                    PO_DATE = DateTime.Now
                };
                list.Add(mVM);
            }
            return list;
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

        private Task<QueryData<MasterVM>> OnQueryAsync(QueryPageOptions options)
        {
            IEnumerable<MasterVM> items = Items;


            // 设置记录总数
            var total = items.Count();

            // 内存分页
            items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();

            return Task.FromResult(new QueryData<MasterVM>()
            {
                Items = items,
                TotalCount = total,
                IsSorted = true,
                IsFiltered = true,
                IsSearch = true
            });
        }

        protected Task<QueryData<DetailVM>> DetailQueryAsync(QueryPageOptions options)
        {
            string PO_NO = searchVM.PO_NO;
            string PO_Item = searchVM.PO_Item;
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


        public class MasterVM
        {
            public string PO_NO { get; set; }
            public string PO_Item { get; set; }
            public string Part_No { get; set; }
            public decimal Qty { get; set; }
            public decimal Recive_Qty { get; set; }
            public decimal Return_Qty { get; set; }
            public DateTime? PO_DATE { get; set; }

            
        }

        public class DetailVM
        {
            public string PO_NO { get; set; }
            public string PO_Item { get; set; }
            public string LotNo { get; set; }
            public string DateCode { get; set; }
            public string Enabled { get; set; }
        }

        public class SearchVM
        {
            public string PO_NO { get; set; }
            public string PO_Item { get; set; }
        }
    }
}
