using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetBreakdownRegisterV2.Utility
{
    public class SetCellToComboBoxCell
    {
        public DataGridView dataGridView;
        public string columnName;
        private string[] dataSource;

        public SetCellToComboBoxCell(DataGridView dataGridView, string columnName, string[] dataSource)
        {
            this.dataGridView = dataGridView;
            this.columnName = columnName;
            this.dataSource = dataSource;
        }

        public void Set() {
            //get the index of the column with the name columnName
            int index = dataGridView.Columns[columnName].Index;
            //update each cell of the datagrid in the wanted column to be a comboboxcell.
            //loop each row
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                //loop each cell in row
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    //if cell is in the right column
                    if (row.Cells[i].ColumnIndex == index)
                    {
                        //get the actual value of the cell
                        string actualValue = row.Cells[i].Value.ToString();
                        //set the cell to a new comboboxcell
                        DataGridViewComboBoxCell cell = new DataGridViewComboBoxCell();
                        row.Cells[i] = cell;

                        //fill the comboboxcell with the dataSource provided
                        foreach (string value in dataSource)
                        {
                            cell.Items.Add(value);
                            if (value.Equals(actualValue))
                            {
                                row.Cells[i].Value = actualValue;
                            }
                        }
                    }
                }
            }
        }
    }
}
