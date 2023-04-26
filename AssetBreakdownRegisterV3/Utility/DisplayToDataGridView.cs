using AssetBreakdownRegisterV2.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetBreakdownRegisterV2.Utility
{
    public class DisplayToDataGridView
    {
        private DataGridView dataGridView;
        private List<BreakdownModel> breakdownList;

        public DisplayToDataGridView(DataGridView dataGridView, List<BreakdownModel> breakdownList)
        {
            this.dataGridView = dataGridView;
            this.breakdownList = breakdownList;
        }

        public void Show(string dataGridViewName)
        {
            //Loop each row of the breakdown list
            foreach (BreakdownModel breakdown in breakdownList)
            {
                //If state is registed, place the rows in the requests datagrid
                if (breakdown.State == "Requested")
                {
                    switch (dataGridViewName)
                    {
                        case ("dataGridMaintenanceRequests"):
                            string[] dataGridRowMaintenance = new string[] { breakdown.Id.ToString(), breakdown.Description, breakdown.Code,
                            breakdown.Category, breakdown.Subcategory, breakdown.Severity.ToString(), breakdown.Status,
                            Math.Floor((DateTime.Now - DateTime.Parse(breakdown.Request_date)).TotalDays).ToString(), breakdown.Observations };

                            dataGridView.Rows.Add(dataGridRowMaintenance);
                            break;

                        case ("dataGridHomeRequests"):
                            string[] dataGridRowHome = new string[] { breakdown.Description, breakdown.Category, breakdown.Subcategory,
                            breakdown.Severity.ToString(),
                            Math.Floor((DateTime.Now - DateTime.Parse(breakdown.Request_date)).TotalDays).ToString(), breakdown.Observations };
                            dataGridView.Rows.Add(dataGridRowHome);
                            break;

                        case ("dataGridHomeTotal"):
                            string[] dataGridRowHomeTotal = new string[] { breakdown.Description, breakdown.Category, breakdown.Subcategory,
                            breakdown.Severity.ToString(), breakdown.Status,
                            Math.Floor((DateTime.Now - DateTime.Parse(breakdown.Request_date)).TotalDays).ToString(), breakdown.Observations };
                            dataGridView.Rows.Add(dataGridRowHomeTotal);
                            break;

                        case ("datagridRegister"):
                            string[] dataGridRow = new string[] { breakdown.Id.ToString(), breakdown.Description, breakdown.Code,
                            breakdown.Category, breakdown.Subcategory, breakdown.Severity.ToString(),
                            Math.Floor((DateTime.Now - DateTime.Parse(breakdown.Request_date)).TotalDays).ToString(), breakdown.Observations };
                            dataGridView.Rows.Add(dataGridRow);
                            break;

                    }
                } else if (breakdown.State == "Noted")
                {
                    switch(dataGridViewName)
                    {
                        case ("dataGridMaintenanceMaintenance"):
                            string[] dataGridRowMaintenance = new string[] { breakdown.Id.ToString(), breakdown.Description, breakdown.Code,
                            breakdown.Category, breakdown.Subcategory, breakdown.Severity.ToString(), breakdown.Status,
                            Math.Floor((DateTime.Now - DateTime.Parse(breakdown.Request_date)).TotalDays).ToString(), breakdown.Observations };
                            dataGridView.Rows.Add(dataGridRowMaintenance);
                            break;

                        case ("dataGridHomeMaintenance"):
                            string[] dataGridRowHome = new string[] { breakdown.Description, breakdown.Category, breakdown.Subcategory,
                            breakdown.Severity.ToString(), breakdown.Status,
                            Math.Floor((DateTime.Now - DateTime.Parse(breakdown.Request_date)).TotalDays).ToString(), breakdown.Observations };
                            dataGridView.Rows.Add(dataGridRowHome);
                            break;

                        case ("dataGridHomeTotal"):
                            string[] dataGridRowHomeTotal = new string[] { breakdown.Description, breakdown.Category, breakdown.Subcategory,
                            breakdown.Severity.ToString(), breakdown.Status,
                            Math.Floor((DateTime.Now - DateTime.Parse(breakdown.Request_date)).TotalDays).ToString(), breakdown.Observations };
                            dataGridView.Rows.Add(dataGridRowHomeTotal);
                            break;
                    }
                } else if (breakdown.State == "Concluded")
                {
                    switch (dataGridViewName)
                    {
                        case ("dataGridHistory"):
                            string[] dataGridRowConcluded = new string[] { breakdown.Description, breakdown.Code,
                            breakdown.Category, breakdown.Subcategory, breakdown.Severity.ToString(), breakdown.Status,
                            breakdown.Request_date, breakdown.Register_date, breakdown.Observations, breakdown.Cost.ToString() };
                            dataGridView.Rows.Add(dataGridRowConcluded);
                            break;
                    }
                } else if(dataGridViewName == "datagridRegister")
                {
                    string[] dataGridRow = new string[] { breakdown.Description, breakdown.Code,
                    breakdown.Category, breakdown.Subcategory, breakdown.Severity.ToString(), breakdown.Status,
                    breakdown.Request_date, breakdown.Register_date, breakdown.Observations };
                    dataGridView.Rows.Add(dataGridRow);
                }
            }
        }
    }
}
