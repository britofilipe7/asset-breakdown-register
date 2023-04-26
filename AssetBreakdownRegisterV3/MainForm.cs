
using AssetBreakdownRegisterV2;
using AssetBreakdownRegisterV2.models;
using AssetBreakdownRegisterV2.Utility;
using AssetBreakdownRegisterV3.Utility;




namespace AssetBreakdownRegister
{
    public partial class MainForm : Form
    {
        //global variables to help moving the form
        bool floatingScreen;
        private Point offset;

        //global variable to check if confirmation boxes should 
        bool confirmationBox = bool.Parse("true");

        // Initialize global variable sqlData
        SqliteDataAccess sqlData = new SqliteDataAccess();

        //set the severity value
        private int severity = Int32.Parse("3");

        //Color list based on the severity value
        List<Color> colorSeverityList;

        //To keep track of the selected panel
        private Panel activePanel;
        private Button activeButton;

        //keep track if the windows is maximized
        private bool isMaximized = false;

        private string[] statusDataSource;

        public MainForm()
        {
            InitializeComponent();
            LoadMenu("all");
        }

        private void LoadStatus()
        {
            //Load the status combobox
            statusDataSource = new string[sqlData.status.Count];
            int Count = 0;
            foreach (StatusModel status in sqlData.status)
            {
                statusDataSource[Count] = status.Name;
                Count++;
            }

        }

        /*Load info for each menu*/
        //Load all the menus
        private void LoadMenu(string menu)
        {
            sqlData.LoadData();
            switch (menu)
            {
                case ("status"):
                    LoadStatus();
                    break;
                case ("home"):
                    LoadHome();
                    break;
                case ("maintenance"):
                    LoadMaintenance();
                    break;
                case ("register"):
                    LoadRegister();
                    break;
                case ("history"):
                    LoadHistory();
                    break;
                case ("settings"):
                    LoadSettings();
                    break;
                case ("all"):
                    LoadStatus();
                    LoadHome();
                    LoadMaintenance();
                    LoadRegister();
                    LoadHistory();
                    LoadSettings();
                    SeverityColorList severityColorList = new SeverityColorList(severity);
                    colorSeverityList = severityColorList.GetList();
                    break;
            }
        }

        //Load the home menu (datagridviews, graphs, labels)
        private void LoadHome()
        {
            //clear the 2 datagridviews in Home menu
            dataGridHomeRequests.Rows.Clear();
            dataGridHomeMaintenance.Rows.Clear();
            dataGridHomeTotal.Rows.Clear();

            DisplayToDataGridView displayToDataGridViewRequests = new DisplayToDataGridView(dataGridHomeRequests, sqlData.breakdown_list);
            displayToDataGridViewRequests.Show(dataGridHomeRequests.Name);

            DisplayToDataGridView displayToDataGridViewMaintenance = new DisplayToDataGridView(dataGridHomeMaintenance, sqlData.breakdown_list);
            displayToDataGridViewMaintenance.Show(dataGridHomeMaintenance.Name);

            DisplayToDataGridView displayToDataGridViewTotal = new DisplayToDataGridView(dataGridHomeTotal, sqlData.breakdown_list);
            displayToDataGridViewTotal.Show(dataGridHomeTotal.Name);

            //order by severity
            dataGridHomeRequests.Sort(dataGridHomeRequests.Columns["SeverityHomeRequests"], System.ComponentModel.ListSortDirection.Descending);
            dataGridHomeMaintenance.Sort(dataGridHomeMaintenance.Columns["SeverityHomeMaintenance"], System.ComponentModel.ListSortDirection.Descending);
            dataGridHomeTotal.Sort(dataGridHomeTotal.Columns["SeverityHomeTotal"], System.ComponentModel.ListSortDirection.Descending);

            //Set the Top boxes (requests and maintenance) numbers
            int requestsCount = dataGridHomeRequests.RowCount;
            int maintenanceCount = dataGridHomeMaintenance.RowCount;
            lblRequestsNumber.Text = requestsCount.ToString();
            lblMaintenanceNumber.Text = maintenanceCount.ToString();
            lblTotalNumber.Text = (requestsCount + maintenanceCount).ToString();

            //Graph
            LoadWeeklyBreakdownChart breakdownChart = new LoadWeeklyBreakdownChart(chartWeeklyBreakdowns, sqlData);
            breakdownChart.Load();

            LoadWeeklyBreakdownCostChart breakdownCostChart = new LoadWeeklyBreakdownCostChart(chartWeeklyBreakdownsCost, sqlData);
            breakdownCostChart.Load();

        }

        //Load the maintenance menu (datagridviews, combobox)
        private void LoadMaintenance()
        {
            //Load the status combobox
            cbMaintenanceStatus.Items.Clear();
            LoadMenu("status");
            cbMaintenanceStatus.Items.AddRange(statusDataSource);

            //clear datagridviews
            dataGridMaintenanceRequests.Rows.Clear();
            dataGridMaintenanceMaintenance.Rows.Clear();

            DisplayToDataGridView displayToDataGridViewRequests = new DisplayToDataGridView(dataGridMaintenanceRequests, sqlData.breakdown_list);
            displayToDataGridViewRequests.Show(dataGridMaintenanceRequests.Name);

            DisplayToDataGridView displayToDataGridViewMaintenance = new DisplayToDataGridView(dataGridMaintenanceMaintenance, sqlData.breakdown_list);
            displayToDataGridViewMaintenance.Show(dataGridMaintenanceMaintenance.Name);

            //sort by severity
            dataGridMaintenanceRequests.Sort(dataGridMaintenanceRequests.Columns["SeverityMaintenanceRequests"], System.ComponentModel.ListSortDirection.Descending);

            SetCellToComboBoxCell setCell = new SetCellToComboBoxCell(dataGridMaintenanceMaintenance, "StatusMaintenance", statusDataSource);
            setCell.Set();

            //sort by severity
            dataGridMaintenanceMaintenance.Sort(dataGridMaintenanceMaintenance.Columns["SeverityMaintenance"], System.ComponentModel.ListSortDirection.Descending);

            //set the colors of cells in the severity column based on their related severity - not working
            //SetCellBackColor setCellColor = new SetCellBackColor(dataGridMaintenanceMaintenance, "SeverityMaintenance", fadeColors);
            //setCellColor.Set();
            
        }

        //Load the register menu (comboboxes, datagridviews)
        private void LoadRegister()
        {
            //Loop each control in panelRegisterChild and clear the textboxes and set index 0 to combobox
            foreach (Control child in panelRegisterChildInputs.Controls)
            {
                if (child is TextBox)
                {
                    ((TextBox)child).Text = string.Empty;
                }

                if (child is ComboBox)
                {
                    ((ComboBox)child).DataSource = null;
                    ((ComboBox)child).Text = string.Empty;
                }
                datagridRegister.Rows.Clear();
            }

            //Fill the combobox of categories
            cbRegisterCategory.DataSource = sqlData.categories.Select(x => x.Name).ToList();

            //Fill the combobox of severity
            cbRegisterSeverity.Items.Clear();
            for (int i = 1; i <= severity; i++)
            {
                cbRegisterSeverity.Items.Add(i);
            }
            cbRegisterSeverity.Sorted = true;

            DisplayToDataGridView displayToDataGridView = new DisplayToDataGridView(datagridRegister, sqlData.breakdown_list);
            displayToDataGridView.Show(datagridRegister.Name);
        }

        //Fills the combobox subcategories each time a new category is selected
        private void RegisterCategoryValueChange(object sender, EventArgs e)
        {
            cbRegisterSubcategory.DataSource = null;
            cbRegisterSubcategory.Text = string.Empty;
            //gets the category selected and continue if there's one selected
            if (cbRegisterCategory.SelectedItem != null)
            {
                string category = cbRegisterCategory.SelectedItem.ToString();
                if (!category.Equals(""))
                {
                    //Get the ID of the category selected
                    long categoryID = Int64.Parse(sqlData.categories.Find(x => x.Name == category).Id.ToString());
                    //Get the subcategories from the respective category
                    cbRegisterSubcategory.DataSource = sqlData.subcategories.FindAll(x => x.CategoryID == categoryID).Select(x => x.Name).ToList();
                }
            }
        }

        //Load the history menu (datagridview)
        private void LoadHistory()
        {
            //clear datagridviews
            dataGridHistory.Rows.Clear();

            DisplayToDataGridView displayToDataGridView = new DisplayToDataGridView(dataGridHistory, sqlData.breakdown_list);
            displayToDataGridView.Show(dataGridHistory.Name);
        }

        //Load the settings menu (comboboxes, datagridviews)
        private void LoadSettings()
        {
            //update combobox with languages list
            cbLanguages.Items.Clear();
            cbLanguages.Items.Add("Português");
            cbLanguages.Items.Add("English");
            cbLanguages.Sorted = true;

            //show severity degrees
            numUpDownSeverity.Value = Int32.Parse("3");


            //Assign the values of the status to the list
            lstStatus.DataSource = null;
            lstStatus.DataSource = sqlData.status.Select(x => x.Name).ToList();

            //Assgin the values of the category to list
            lstCategories.DataSource = null;
            lstCategories.DataSource = sqlData.categories.Select(x => x.Name).ToList();
            lstSubcategories.DataSource = null;

            //Misc Settings
            chkConfirmationBoxSetting.Checked = bool.Parse("true");
        }

        /*Menu bar selection*/
        //Helper function, sets activePanel as the current active panel and brings that panel to the front
        private void OpenChildForm(Panel childPanel, object sender)
        {
            activePanel = childPanel;
            childPanel.BringToFront();
            childPanel.Show();

            if (activeButton != null)
            {
                activeButton.BackColor = panelMenu.BackColor;
            }

            activeButton = (Button)sender;
            activeButton.BackColor = Color.FromArgb(66, 90, 115);

        }

        //Function for clicking event of the menu bar buttons, depending on the button clicked it displays the respective panel
        private void SelectPanel(object sender, EventArgs e)
        {
            string btn = ((Button)sender).Name;
            switch (btn)
            {
                case ("btnHome"):
                    OpenChildForm(panelHome, sender);
                    LoadHome();
                    break;
                case ("btnMaintenance"):
                    OpenChildForm(panelMaintenance, sender);
                    LoadMaintenance();
                    break;
                case ("btnRegister"):
                    OpenChildForm(panelRegister, sender);
                    LoadRegister();
                    break;
                case ("btnHistory"):
                    OpenChildForm(panelHistory, sender);
                    LoadHistory();
                    break;
                case ("btnSettings"):
                    OpenChildForm(panelSettings, sender);
                    LoadSettings();
                    break;
            }
        }

        /*Datagridview selection in the Home panel*/
        //Home Panel variables to control which datagridview to show, and sets the colors for selected and deselected
        private Panel homeSelectedPanel;
        private Color homeTopPanelSelected = Color.FromArgb(2, 97, 174);
        private Color homeTopPanelDeselected = Color.FromArgb(32, 48, 64);
        private DataGridView homeSelectedData;

        //Helper function for Home panel, sets the top color and which datagridview to display
        private void SelectHomeView(Panel currentPanel, DataGridView currentDataGrid)
        {
            //If there's already a selectedPanel set it's color to homeTopPanelDeselected
            if (homeSelectedPanel != null)
            {
                homeSelectedPanel.BackColor = homeTopPanelDeselected;
            }
            //Set the Select Panel to currentPanel and the homeTopPanelSelected color
            homeSelectedPanel = currentPanel;
            currentPanel.BackColor = homeTopPanelSelected;

            //makes the currentDataGrid visible
            if (homeSelectedData != null)
            {
                homeSelectedData.Visible = false;
            }
            homeSelectedData = currentDataGrid;
            currentDataGrid.Visible = true;
        }

        private void HomeRequests_Click(object sender, EventArgs e)
        {
            SelectHomeView(panelRequestsTop, dataGridHomeRequests);
        }

        private void HomeMaintenance_Click(object sender, EventArgs e)
        {
            SelectHomeView(panelMaintenanceTop, dataGridHomeMaintenance);
        }

        private void HomeTotal_Click(object sender, EventArgs e)
        {
            SelectHomeView(panelTotalTop, dataGridHomeTotal);
        }



        /*Events in Register panel*/
        private void btnRegisterRegister_Click(object sender, EventArgs e)
        {
            //assign severity variable
            string severity = cbRegisterSeverity.Text;

            //make sure description or severity are not empty
            if (txtRegisterDescription.Text.Trim().Equals("") || severity.Equals(""))
            {
                MessageBox.Show("Os campos descrição e severidade são de preenchimento obrigatório.");
                return;
            }

            int severityColor = colorSeverityList[Int32.Parse(severity) - 1].ToArgb();

            //build a BreakdownModel model
            BreakdownModel model = new BreakdownModel()
            {
                Description = txtRegisterDescription.Text,
                Code = txtRegisterCode.Text,
                Category = cbRegisterCategory.Text,
                Subcategory = cbRegisterSubcategory.Text,
                Request_date = DateTime.Now.ToString(),
                Status = "",
                Register_date = "",
                Severity = Int32.Parse(cbRegisterSeverity.Text),
                Observations = txtRegisterObservations.Text,
                State = "Requested",
                Color = severityColor.ToString(),
            };

            //insert the model to the database
            sqlData.InsertData(model);
            LoadMenu("register");
        }

        private void btnRegisterReset_Click(object sender, EventArgs e)
        {
            LoadMenu("register");
        }

        private void btnRegisterRemove_Click(object sender, EventArgs e)
        {
            //Delete the request from the database
            string selectedID = (string)datagridRegister.CurrentRow.Cells["IDRegister"].Value;
            //Create a confirmationBox object and proceed only if the setting is true and the user select Yes
            if (confirmationBox)
            {
                string message = $"Deseja eliminar o pedido {selectedID}";
                ConfirmationBox confirmationBox = new ConfirmationBox(message);
                if (!confirmationBox.Ask())
                {
                    return;
                }
            }
            sqlData.DeleteData("breakdown", "id", selectedID);
            LoadMenu("register");

        }

        //Settings buttons
        private void AddNewStatus(object sender, EventArgs e)
        {
            //Build new StatusModel
            StatusModel model = new StatusModel()
            {
                Name = txtSettingsStatus.Text
            };
            //Insert into the database
            sqlData.InsertData(model);
            txtSettingsStatus.ResetText();
            LoadMenu("settings");
        }

        private void RemoveStatus(object sender, EventArgs e)
        {
            //Delete status from the database
            string selectedToDelete = lstStatus.SelectedItem.ToString();
            //Create a confirmationBox object and proceed only if the setting is true and the user select Yes
            if (confirmationBox)
            {
                string message = $"Deseja eliminar o status {selectedToDelete}";
                ConfirmationBox confirmationBox = new ConfirmationBox(message);
                if (!confirmationBox.Ask())
                {
                    return;
                }
            }
            sqlData.DeleteData("status", "name", selectedToDelete);
            LoadMenu("settings");
        }

        private void AddCategory(object sender, EventArgs e)
        {
            //Build new CategoryModel
            CategoryModel model = new CategoryModel()
            {
                Name = txtSettingsNewCategory.Text,
            };
            //Insert into the database
            sqlData.InsertData(model);
            txtSettingsNewCategory.ResetText();
            LoadMenu("settings");

        }

        private void AddSubcategory(object sender, EventArgs e)
        {
            string subcategory = txtSettingsNewSubCategory.Text;
            //Ensures that a category is selected
            if (lstCategories.SelectedItems.Count == 0)
            {
                MessageBox.Show("Por favor selecione uma categoria.");
                txtSettingsNewSubCategory.ResetText();
                return;
            }
            string category = lstCategories.SelectedItem.ToString();
            //Get the ID of the category
            int categoryID = Int32.Parse(sqlData.categories.Find(x => x.Name == category).Id.ToString());

            SubcategoryModel model = new SubcategoryModel()
            {
                Name = subcategory,
                CategoryID = categoryID,
            };
            //Insert into the database
            sqlData.InsertData(model);
            txtSettingsNewSubCategory.ResetText();
            LoadMenu("settings");
        }

        //Function to display the listbox subcategory each time a new category is selected in the listbox
        private void SelectedCategoryChanged(object sender, EventArgs e)
        {
            lstSubcategories.DataSource = null;
            if (lstCategories.SelectedIndex == -1)
            {
                return;
            }

            string category = lstCategories.SelectedItem.ToString();
            //get category ID based on the name
            int categoryID = Int32.Parse(sqlData.categories.Find(x => x.Name == category).Id.ToString());

            lstSubcategories.DataSource = sqlData.subcategories.FindAll(x => x.CategoryID == categoryID).Select(x => x.Name).ToList();
            btnSettingsRemoveSubcategory.Visible = false;
        }

        //make the button for deleting a subcategory visible each time a subcategory is selected
        private void SelectedSubCategoryChanged(object sender, EventArgs e)
        {
            if (lstSubcategories.SelectedIndex != -1)
            {
                btnSettingsRemoveSubcategory.Visible = true;
            }
        }

        private void DeleteCategories(object sender, EventArgs e)
        {
            string category = lstCategories.SelectedItem.ToString();
            //Create a confirmationBox object and proceed only if the setting is true and the user select Yes
            if (confirmationBox)
            {
                string message = $"Deseja eliminar a categoria {category}";
                ConfirmationBox confirmationBox = new ConfirmationBox(message);
                if (!confirmationBox.Ask())
                {
                    return;
                }
            }
            sqlData.DeleteData("category", "name", category);
            LoadMenu("settings");
        }

        private void DeleteSubcategories(object sender, EventArgs e)
        {
            string subcategory = lstSubcategories.SelectedItem.ToString();
            //Create a confirmationBox object and proceed only if the setting is true and the user select Yes
            if (confirmationBox)
            {
                string message = $"Deseja eliminar a subcategoria {subcategory}";
                ConfirmationBox confirmationBox = new ConfirmationBox(message);
                if (!confirmationBox.Ask())
                {
                    return;
                }
            }
            sqlData.DeleteData("subcategory", "name", subcategory);
            LoadMenu("settings");
        }

        //update severity value in app.config
        private void btnSettingsSeveritySave_Click(object sender, EventArgs e)
        {
            SaveAppConfigSettings settings = new SaveAppConfigSettings("severity", "numUpDownSeverity.Value.ToString()");
            settings.Save();
        }

        private void btnMaintenanceRegister_Click(object sender, EventArgs e)
        {
            //Declare the variable status to later insert into the db
            string status = cbMaintenanceStatus.Text;
            if (status == string.Empty)
            {
                MessageBox.Show("Por favor insira um stauts válido.");
                return;
            }
            //Is some row is selected
            if (dataGridMaintenanceRequests.CurrentRow != null)
            {
                string selectedID = (string)dataGridMaintenanceRequests.CurrentRow.Cells["IDMaintenanceRequests"].Value;
                //UPDATE the table breakdown with the status defined by the user
                sqlData.UpdateData("breakdown", "status", "state", "register_date", status, "Noted", DateTime.Now.ToString(), selectedID);
                LoadMenu("maintenance");
            }
        }

        private void btnMaintenanceReset_Click(object sender, EventArgs e)
        {
            LoadMenu("maintenance");
        }

        private void btnMaintenanceSave_Click(object sender, EventArgs e)
        {
            string id = dataGridMaintenanceMaintenance.SelectedRows[0].Cells[0].Value.ToString();
            string status = dataGridMaintenanceMaintenance.SelectedRows[0].Cells[6].Value.ToString();
            sqlData.UpdateData("breakdown", "status", status, id);
            LoadMenu("maintenance");


        }

        private void btnMaintenanceConclude_Click(object sender, EventArgs e)
        {
            //to do add observatons
            string id = dataGridMaintenanceMaintenance.SelectedRows[0].Cells[0].Value.ToString();
            string observations = dataGridMaintenanceMaintenance.SelectedRows[0].Cells[8].Value.ToString();
            CostForm costForm = new CostForm(id, observations);
            costForm.ShowDialog();
            //sqlData.UpdateData("breakdown", "state", "Concluded", id);
            LoadMenu("maintenance");

        }

        private void TopMouseDown_Event(object sender, MouseEventArgs e)
        {
            //set the offset variable with the position of the actual mouse
            offset.X = e.X;
            offset.Y = e.Y;
            floatingScreen = true;
        }

        private void TopMouseMove_Event(object sender, MouseEventArgs e)
        {
            if (floatingScreen == true)
            {
                //set the position of the form based on the actual mouse position and the offset
                Point currentScreenPos = PointToScreen(e.Location);
                this.Location = new Point(currentScreenPos.X - offset.X, currentScreenPos.Y - offset.Y);
            }
        }

        private void TopMouseUp_Event(object sender, MouseEventArgs e)
        {
            floatingScreen = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }

        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnMiscSettingsSave_Click(object sender, EventArgs e)
        {
            if (chkConfirmationBoxSetting.Checked)
            {
                SaveAppConfigSettings settings = new SaveAppConfigSettings("confirmationBox", "true");
                settings.Save();
            }
            else
            {
                SaveAppConfigSettings settings = new SaveAppConfigSettings("confirmationBox", "false");
                settings.Save();
            }
        }

        private void btnHistoryExportExcel_Click(object sender, EventArgs e)
        {
            // creating Excel Application
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application  
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // creating new Excelsheet in workbook  
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            // store its reference to worksheet  
            worksheet = workbook.ActiveSheet;
            // storing header part in Excel  
            for (int i = 1; i < dataGridHistory.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataGridHistory.Columns[i - 1].HeaderText;
            }
            // storing Each row and column value to excel sheet  
            for (int i = 0; i < dataGridHistory.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridHistory.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridHistory.Rows[i].Cells[j].Value.ToString();
                }
            }
            // see the excel sheet behind the program  
            app.Visible = true;
        }

        private void btnClearDB_Click(object sender, EventArgs e)
        {
            sqlData.ClearDB();
        }
    }
}
