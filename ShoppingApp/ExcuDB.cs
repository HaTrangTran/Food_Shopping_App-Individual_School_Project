using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace ShoppingApp
{
    class ExcuDB
    {
        ConnectionDB con = new ConnectionDB();
        ConnectionDB con1 = new ConnectionDB();
        ConnectionDB con2 = new ConnectionDB();
        ConnectionDB con3 = new ConnectionDB();
        ConnectionDB con4 = new ConnectionDB();

        public string cost = "0.00";

        SqlDataReader Rd_UserStore;
        SqlDataReader Rd_Check;

        //-------------------------------------START TAB CREATE NEW PLAN MEAL---------------------------------------
        public void CreateNewPlan()
        {
            DialogResult result = MessageBox.Show("Do you wan to create a new Plan meal?", "New Plan meal!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            string str;
            if (result == DialogResult.Yes)
            {
                con.Open();
                str = "Delete from PlannedMeal";
                con.ExecuteNonQuery(str);
                str = "Delete from ShoppingList";
                con.ExecuteNonQuery(str);
                con.Close();
            }
        }

        public void CreatShoppingList()
        {
            string st = "select * from PlannedMeal";
            SqlDataReader R;
            con.Open();
            R = con.ExecuteReader(st);
            if (R.HasRows)
            {
                con1.Open();
                con2.Open();
                string Unit = " ";
                SqlDataReader Rd_Unit;
                SqlDataReader Rd;
                SqlDataReader Rd_UserStore;
                SqlDataReader Rd_Check;

                Boolean dk = false;
                int temp = 0;

                String str = "Delete from ShoppingList";
                con1.ExecuteNonQuery(str);

                string sql = "Select SUBQUERY.IngredientName, Sum(SUBQUERY.QuantityShopping) as SumOfQuantity From (Select Ingredients.RecipeID, Ingredients.IngredientName, Ingredients.Quantity * PlannedMeal.PlannedPortion as QuantityShopping, Ingredients.Unit From Ingredients, PlannedMeal  Where Ingredients.RecipeID = PlannedMeal.RecipeID) SUBQUERY Group By SUBQUERY.IngredientName";
                Rd = con1.ExecuteReader(sql);

                String st_UserStore = "Select IngredientName, Quantity From SupplyItems where Quantity >0";
                Rd_UserStore = con2.ExecuteReader(st_UserStore);

                if (Rd_UserStore.HasRows)
                {

                    while (Rd.Read())
                    {
                        dk = false;
                        con4.Open();
                        string st_Check = "Select IngredientName, Quantity From SupplyItems where Quantity >0";
                        Rd_Check = con4.ExecuteReader(st_Check);
                        while (Rd_Check.Read())
                        {
                            if ((String.Compare(Rd["IngredientName"].ToString().Trim().ToUpper(), Rd_Check["IngredientName"].ToString().Trim().ToUpper(), true) == 0) && (Convert.ToInt32(Rd["SumOfQuantity"]) - Convert.ToInt32(Rd_Check["Quantity"]) > 0))
                            {
                                temp = Convert.ToInt32(Rd["SumOfQuantity"]) - Convert.ToInt32(Rd_Check["Quantity"]);

                                con3.Open();
                                str = "Select Unit From Ingredients Where IngredientName = '" + Rd["IngredientName"].ToString() + "'";
                                Rd_Unit = con3.ExecuteReader(str);
                                while (Rd_Unit.Read())
                                {
                                    Unit = Rd_Unit["Unit"].ToString();
                                }
                                con3.Close();
                                con3.Open();
                                str = "insert into ShoppingList values('" + Rd["IngredientName"].ToString() + "'," + temp + ", '" + Unit + "')";
                                con3.ExecuteNonQuery(str);
                                dk = true;
                                con3.Close();
                            }
                            if ((String.Compare(Rd["IngredientName"].ToString().Trim().ToUpper(), Rd_Check["IngredientName"].ToString().Trim().ToUpper(), true) == 0) && (Convert.ToInt32(Rd["SumOfQuantity"]) - Convert.ToInt32(Rd_Check["Quantity"]) <= 0))
                            {
                                dk = true;
                            }
                        }
                        con4.Close();

                        if (dk == false)
                        {
                            con4.Open();
                            con3.Open();
                            str = "Select Unit From Ingredients Where IngredientName = '" + Rd["IngredientName"].ToString() + "'";
                            Rd_Unit = con3.ExecuteReader(str);
                            while (Rd_Unit.Read())
                            {
                                Unit = Rd_Unit["Unit"].ToString();
                            }
                            con3.Close();

                            str = "insert into ShoppingList values('" + Rd["IngredientName"].ToString() + "'," + Convert.ToInt32(Rd["SumOfQuantity"]) + ",'" + Unit + "')";
                            con4.ExecuteNonQuery(str);
                            con4.Close();
                        }

                    }
                }
                else

                {
                    while (Rd.Read())
                    {
                        con3.Open();
                        str = "Select Unit From Ingredients Where IngredientName = '" + Rd["IngredientName"].ToString() + "'";
                        Rd_Unit = con3.ExecuteReader(str);
                        while (Rd_Unit.Read())
                        {
                            Unit = Rd_Unit["Unit"].ToString();
                        }
                        con3.Close();
                        con4.Open();
                        str = "insert into ShoppingList values('" + Rd["IngredientName"].ToString() + "'," + Convert.ToInt32(Rd["SumOfQuantity"]) + ",'" + Unit + "')";
                        con4.ExecuteNonQuery(str);
                        con4.Close();
                    }

                }
            }

            else
            {
                MessageBox.Show("No Data in PlannedMeal table!");
            }
            con.Close();
            con1.Close();
            con2.Close();
        }


        public void AddItemToPlan(int RecipeID, string RecipeName, int NumPortion)
        {
            con.Open();
            con1.Open();
            Boolean Upd = false;
            string Mystr = "Select RecipeID, PlannedPortion from PlannedMeal";
            SqlDataReader Rd_Update;
            Rd_Update = con.ExecuteReader(Mystr);
            if (Rd_Update.HasRows)
            {
                while (Rd_Update.Read())
                {
                    if (Convert.ToInt32(Rd_Update["RecipeID"]) == RecipeID)
                    {
                        int Temp = Convert.ToInt32(Rd_Update["PlannedPortion"]) + NumPortion;
                        string st = "Update PlannedMeal Set PlannedPortion = " + Temp + "where RecipeID = " + Rd_Update["RecipeID"];
                        con1.ExecuteNonQuery(st);
                        Upd = true;
                    }
                }
                if (Upd == false)
                {
                    string str = "insert into PlannedMeal values(" + RecipeID + ",'" + RecipeName + "'," + NumPortion + "," + 0 + ")";
                    con1.ExecuteNonQuery(str);

                }
            }
            else
            {
                string str = "insert into PlannedMeal values(" + RecipeID + ",'" + RecipeName + "'," + NumPortion + "," + 0 + ")";
                con1.ExecuteNonQuery(str);
            }
            con.Close();
            con1.Close();
        }


        public void RemoveItemFromPlan(string MyCellValue)
        {
            DialogResult result = MessageBox.Show("Do you wan to Delete an item in Plan meal?", "Delete an item in Plan meal!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            string str;
            if (result == DialogResult.Yes)
            {
                con.Open();
                str = "Delete from PlannedMeal where RecipeName = '" + MyCellValue + "'";
                con.ExecuteNonQuery(str);
                con.Close();
            }

        }


        //--------------------START METHODS OF TAB SUPPLY ITEMS------------------------------

        public void InsertDataToSupplyItems(string SelectIngName, string SelectIngCategory, string TypeIngName, string TypeIngCategory, string IngUnit, int numIngNet, Decimal IngPrice, string IngrLocation, string SupDate, string ExpDate, Boolean RdSelectIngName, Boolean RdSelectIngCategoty, Boolean RdTypeIngName, Boolean RdTypeIngCategory)
        {
            string IngName = null;
            string IngCate = null;

            if (RdSelectIngName == true && SelectIngName != null)
            {
                if (RdSelectIngCategoty == true && SelectIngCategory != null)
                {
                    IngName = SelectIngName;
                    IngCate = SelectIngCategory;
                }
                else
                {
                    IngName = SelectIngName;
                    IngCate = TypeIngCategory;
                }
            }
            if (RdTypeIngName == true && TypeIngName != null)
            {
                if (RdSelectIngCategoty == true && SelectIngCategory != null)
                {
                    IngName = TypeIngName;
                    IngCate = SelectIngCategory;
                }
                else
                {
                    IngName = TypeIngName;
                    IngCate = TypeIngCategory;
                }
            }
            if (IngName != null)
            {
                con.Open();
                con1.Open();
                con2.Open();
                con3.Open();
                string str = "Select IngredientName, Quantity from SupplyItems where IngredientName ='" + IngName.Trim() + "'";
                string Mystr;
                SqlDataReader Rd;
                Rd = con.ExecuteReader(str);
                if (Rd.HasRows)
                {
                    int temp = numIngNet + Convert.ToInt32(Rd["Quantity"]);
                    Mystr = "Update SupplyItems Set Quantity =" + temp + ", Unit ='" + IngUnit + "', DateOfArrival ='" + SupDate + "',ExpireDate ='" + ExpDate + "', Price =" + IngPrice + ", IngreCategory ='" + IngCate.Trim() + "' where IngredientName ='" + IngName.Trim() + "'";
                    con3.ExecuteNonQuery(Mystr);

                }
                else
                {
                    Mystr = "insert into SupplyItems values('" + IngName + "'," + numIngNet + ",'" + IngUnit + "','" + SupDate + "','" + ExpDate + "'," + IngPrice + ",'" + IngCate + "')";
                    con3.ExecuteNonQuery(Mystr);

                }
                con3.Close();

                int IngrID = 1;
                str = "Select IngredientID from SupplyItems where IngredientName ='" + IngName.Trim() + "'";
                SqlDataReader R;
                R = con1.ExecuteReader(str);
                if (R.HasRows)
                {
                    while (R.Read())
                    {
                        IngrID = Convert.ToInt32(R["IngredientID"]);

                    }
                }

                str = "Select IngredientID from Location where IngredientID =" + IngrID;
                SqlDataReader MyRd;
                MyRd = con2.ExecuteReader(str);
                con3.Open();
                if (MyRd.HasRows)
                {
                    if (IngrLocation != null)
                    {
                        Mystr = "Update Location Set LocationName ='" + IngrLocation + "' where IngredientID =" + IngrID;
                        con3.ExecuteNonQuery(Mystr);
                    }
                }
                else
                {
                    Mystr = "insert into Location values(" + IngrID + ",'" + IngrLocation + "')";
                    con3.ExecuteNonQuery(Mystr);
                }
                con.Close();
                con3.Close();
                
                decimal Cost;
                if ((String.Compare(IngUnit.ToString().Trim(), "gram", true) == 0) || (String.Compare(IngUnit.ToString().Trim(), "ml", true) == 0))
                {
                    Cost = (numIngNet * IngPrice) / 1000;
                }
                else
                {
                    Cost = (numIngNet * IngPrice);
                }
                Mystr = "insert into Accounting values('" + IngName + "'," + numIngNet + ",'" + IngUnit + "','" + SupDate + "','" + ExpDate + "'," + IngPrice + ",'" + IngCate + "'," + Cost + ")";
                con.Open();
                con.ExecuteNonQuery(Mystr);


                con.Close();
                con1.Close();
                con2.Close();
            }
            else
            {
                MessageBox.Show("You have to input the name of Ingredient in order to Insert!");
            }
        }


        public void DeleteItemFromSupplyItems(int IngredientID)
        {
            DialogResult result = MessageBox.Show("Do you wan to Delete an item in Supply Items?", "Delete an item in Supply Items!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (IngredientID != 0)
                {
                    con.Open();
                    string st = "Delete From SupplyItems where IngredientID =" + IngredientID;
                    con.ExecuteNonQuery(st);

                    st = "Delete From Location where IngredientID =" + IngredientID;
                    con.ExecuteNonQuery(st);
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Let's click on an item to Delete!");
                }

            }
        }


        public void UpdateItemFromSupplyItems(int IngredientID, string IngName, int IngNet, string IngUnit, string IngLocation, string IngSupDate, string IngExpDate, Decimal IngPrice, string IngCategory)
        {
            DialogResult result = MessageBox.Show("Do you want to Update an item in Supply Items?", "Update an item in Supply Items!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (IngredientID != 0)
                {
                    con.Open();
                    string st = "Update SupplyItems Set IngredientName ='" + IngName + "', Quantity =" + IngNet + ", Unit ='" + IngUnit + "', DateOfArrival ='" + IngSupDate + "',ExpireDate ='" + IngExpDate + "', Price =" + IngPrice + ", IngreCategory ='" + IngCategory + "' where IngredientID =" + IngredientID;
                    con.ExecuteNonQuery(st);

                    st = "Update Location Set LocationName ='" + IngLocation + "' where IngredientID =" + IngredientID;
                    con.ExecuteNonQuery(st);

                    decimal Cost;
                    string sql = "SELECT * FROM Accounting WHERE IngredientName ='" + IngName + "' AND DateOfArrival = '" + IngSupDate + "'";
                    SqlDataReader Rd;
                    Rd = con.ExecuteReader(sql);

                    if (Rd.HasRows)
                    {
                        while (Rd.Read())
                        {
                            if ((String.Compare(IngUnit.ToString().Trim(), "gram", true) == 0) || (String.Compare(IngUnit.ToString().Trim(), "ml", true) == 0))
                            {
                                Cost = (Convert.ToInt32(Rd["Quantity"]) * IngPrice) / 1000;
                            }
                            else
                            {
                                Cost = Convert.ToInt32(Rd["Quantity"]) * IngPrice;
                            }
                            string str = "Update Accounting Set Price =" + IngPrice + ", IngreCategory ='" + IngCategory + "', Cost = " + Cost + " where IngredientName ='" + IngName + "' AND Quantity = '" + Convert.ToInt32(Rd["Quantity"]) + "' AND DateOfArrival = '" + IngSupDate + "' AND Price = 0.00";
                            con1.Open();
                            con1.ExecuteNonQuery(str);
                            con1.Close();
                        }
                    }
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Click on an item to Update!");
                }
            }
        }


        //--------------------START METHODS OF TAB CONFIRMED PORTION------------------------------

        public void ConfirmUsedPortion(int RecipeID, string RecipeName, int UsedPortion)
        {
            string notify = " ";
            if (UsedPortion != 0)
            {
                con.Open();
                con2.Open();

                string Mystr = "Select PlannedPortion, UsedPortion from PlannedMeal where RecipeID =" + RecipeID;
                SqlDataReader Rd_Update;
                Rd_Update = con.ExecuteReader(Mystr);
                if (Rd_Update.HasRows)
                {
                    while (Rd_Update.Read())
                    {
                        int temp = Convert.ToInt32(Rd_Update["UsedPortion"]);
                        temp += UsedPortion;
                        if (Convert.ToInt32(Rd_Update["PlannedPortion"]) < temp)
                        {
                            MessageBox.Show("Used Portion is more than Planned Portion! Please enter smaller number!");
                        }
                        else
                        {
                            string st = "Update PlannedMeal Set UsedPortion =" + temp + " where RecipeID =" + RecipeID;
                            con2.ExecuteNonQuery(st);


                            con3.Open();
                            con1.Open();

                            SqlDataReader Rd;
                            SqlDataReader Rd_UserStore;

                            Boolean dk = false;
                            Boolean dkNotify = false;
                            int tempQuantity = 0;

                            string sql = "SELECT RecipeID, IngredientName, Quantity, Unit FROM Ingredients  WHERE RecipeID =" + RecipeID;
                            Rd = con1.ExecuteReader(sql);

                            if (Rd.HasRows)
                            {
                                while (Rd.Read())
                                {
                                    int usedQntity = UsedPortion * Convert.ToInt32(Rd["Quantity"]);
                                    dk = false;
                                    dkNotify = false;
                                    con4.Open();
                                    string st_UserStore = "Select IngredientName, Quantity From SupplyItems";
                                    Rd_UserStore = con4.ExecuteReader(st_UserStore);
                                    while (Rd_UserStore.Read())
                                    {
                                        if (String.Compare(Rd["IngredientName"].ToString().Trim().ToUpper(), Rd_UserStore["IngredientName"].ToString().Trim().ToUpper(), true) == 0)
                                        {
                                            if (Convert.ToInt32(Rd_UserStore["Quantity"]) - usedQntity < 0)
                                            {
                                                notify += "Not enough quantity for " + Rd["IngredientName"].ToString().Trim() + "!\n";
                                                tempQuantity = 0;
                                                string strg = "Update SupplyItems Set Quantity = " + tempQuantity + "where IngredientName = '" + Rd["IngredientName"].ToString() + "'";
                                                con3.ExecuteNonQuery(strg);
                                                dkNotify = true;
                                            }
                                            else
                                            {
                                                tempQuantity = Convert.ToInt32(Rd_UserStore["Quantity"]) - usedQntity;
                                                string stg = "Update SupplyItems Set Quantity = " + tempQuantity + "where IngredientName = '" + Rd["IngredientName"].ToString() + "'";
                                                con3.ExecuteNonQuery(stg);
                                                //notify += "";
                                            }
                                            dk = true;
                                            break;
                                        }
                                    }
                                    con4.Close();

                                    if (dk == false)
                                    {
                                        dkNotify = true;
                                        notify += "No " + Rd["IngredientName"].ToString().Trim().ToUpper() + " in the storage!\n";
                                    }
                                }
                                if (dkNotify == false)
                                    MessageBox.Show("Enough ingredients in the storage for confirmed portions!");
                                else
                                    MessageBox.Show(notify);
                            }
                            else
                            {
                                MessageBox.Show("No food items in the storage!");
                            }
                            con1.Close();
                            con3.Close();
                        }
                    }
                }
                con2.Close();
                con.Close();
            }
            else
            {
                MessageBox.Show("Please give value to Used Portion to Update!");
            }
        }

        //---------------------------------------------------------ACCOUNTING-------------------------------------------------------------------
        public string Accounting(DataGridView MyGrid, string StartDate, string EndDate)
        {
            con.Open();
            DataSet ds = new DataSet();
            string str = "Select * from Accounting where DateOfArrival between '" + StartDate + "' and '" + EndDate + "'";
            ds = con.ExecuteDataSet(str);
            MyGrid.DataSource = ds.Tables[0];
            con.Close();

            str = "Select SUM(Cost) as TotalCost from Accounting where DateOfArrival between '" + StartDate + "' and '" + EndDate + "'";
            con.Open();
            SqlDataReader Rd;
            Rd = con.ExecuteReader(str);
            if (Rd.HasRows)
            {
                while (Rd.Read())
                {
                    cost = Rd["TotalCost"].ToString();
                }

            }
            else
            {
                cost = "0.00";
                MessageBox.Show("There are no food items bought in given time period!");
            }
            return cost;
            con.Close();
        }

        //---------------------------------------------------------ADMIN_CATEGORY-------------------------------------------------------------------

        public void AddCategory_Admin(string CategoryName)
        {
            con.Open();
            con1.Open();
            Boolean Upd = false;

            string Mystr = "Select CategoryName from RecipeCategory";
            SqlDataReader Rd;
            Rd = con.ExecuteReader(Mystr);
            if (Rd.HasRows)
            {
                while (Rd.Read())
                {
                    if (Rd["CategoryName"].ToString().Trim() == CategoryName)
                    {
                        MessageBox.Show("Existing Category Name!");
                        Upd = true;
                    }
                }
                if (Upd == false)
                {
                    string str = "INSERT INTO RecipeCategory VALUES('" + CategoryName + "')";
                    con1.ExecuteNonQuery(str);
                }
            }
            else
            {
                string str = "INSERT INTO RecipeCategory VALUES('" + CategoryName + "')";
                con1.ExecuteNonQuery(str);
            }
            con.Close();
            con1.Close();
        }

        public void UpdateCategory_Admin(int CateID, string CateName)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to Update?", "Update Category in Recipe Category!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (CateID != 0)
                {
                    con.Open();
                    string st = "UPDATE RecipeCategory SET CategoryName ='" + CateName + "' WHERE CategoryID =" + CateID;
                    con.ExecuteNonQuery(st);
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Click on a Category to Update!");
                }
            }
        }

        public void DelCategory_Admin(int CateID)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to Delete?", "Delete Category in Recipe Category!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (CateID != 0)
                {
                    string str = "Select CategoryID from Recipe where CategoryID =" + CateID;
                    con.Open();
                    SqlDataReader Rd;
                    Rd = con.ExecuteReader(str);
                    if (Rd.HasRows)
                    {
                        MessageBox.Show("Can not Delete!\nExisting one or more recipes in selected Category!");
                    }
                    else
                    {
                        con1.Open();
                        string st = "DELETE FROM RecipeCategory WHERE CategoryID =" + CateID;
                        con1.ExecuteNonQuery(st);
                        con1.Close();
                    }
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Click on an item to Delete!");
                }

            }
        }

        //---------------------------------------------------------ADMIN_RECIPE-------------------------------------------------------------------

        public void AddRecipe_Admin(string RecipeName, int CateID, string Instruction, string EstTime)
        {
            con.Open();
            con1.Open();
            Boolean Upd = false;

            string Mystr = "Select RecipeName, CategoryID from Recipe";
            SqlDataReader Rd;
            Rd = con.ExecuteReader(Mystr);
            if (Rd.HasRows)
            {
                while (Rd.Read())
                {
                    if (Rd["RecipeName"].ToString().Trim() == RecipeName)
                    {
                        if (Convert.ToInt32(Rd["CategoryID"]) == CateID)
                        {
                            MessageBox.Show("Existing Recipe!");
                            Upd = true;
                        }
                        else
                        {
                            string str = "INSERT INTO Recipe VALUES('" + RecipeName + "'," + CateID + ",'" + Instruction + "','" + EstTime + "')";
                            con1.ExecuteNonQuery(str);
                            Upd = true;
                        }
                    }
                }
                if (Upd == false)
                {
                    string str = "INSERT INTO Recipe VALUES('" + RecipeName + "'," + CateID + ",'" + Instruction + "','" + EstTime + "')";
                    con1.ExecuteNonQuery(str);
                }
            }
            else
            {
                string str = "INSERT INTO Recipe VALUES('" + RecipeName + "'," + CateID + ",'" + Instruction + "','" + EstTime + "')";
                con1.ExecuteNonQuery(str);
            }
            con.Close();
            con1.Close();
        }

        public void UpdateRecipe_Admin(int RecipeID, string RecipeName, int CateID, string Instruction, string EstTime)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to Update?", "Update Recipe!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (RecipeID != 0)
                {
                    con.Open();
                    string st = "UPDATE Recipe SET RecipeName = '" + RecipeName + "', CategoryID = " + CateID + ", Instructions = '" + Instruction + "', EstimatedTime = '" + EstTime + "' WHERE RecipeID =" + RecipeID;
                    con.ExecuteNonQuery(st);
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Click on a Category to Update!");
                }
            }
        }

        public void DelRecipe_Admin(int RecipeID)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to Delete?", "Delete Recipe!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (RecipeID != 0)
                {
                    con.Open();
                    string st = "DELETE FROM Recipe WHERE RecipeID =" + RecipeID;
                    con.ExecuteNonQuery(st);
                    con.Close();

                    con.Open();
                    st = "DELETE FROM Ingredients WHERE RecipeID =" + RecipeID;
                    con.ExecuteNonQuery(st);
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Click on a Recipe to Delete!");
                }

            }
        }

        //---------------------------------------------------------ADMIN_INGREDIENTS-------------------------------------------------------------------

        public void AddIngredient_Admin(int RecipeID, Boolean RadSelectName, string SelectIngName, Boolean RadTypeName, string TypeIngName, int Net, string Unit)
        {
            string IngName = null;

            if (RadSelectName == true && SelectIngName != null)
            {
                IngName = SelectIngName;
            }
            else
            {
                IngName = TypeIngName;
            }

            if (IngName != null)
            {
                con.Open();
                con1.Open();

                string str = "SELECT IngredientName FROM Ingredients WHERE RecipeID = " + RecipeID + " AND IngredientName ='" + IngName.Trim() + "'";
                string Mystr;
                SqlDataReader Rd;
                Rd = con.ExecuteReader(str);
                if (Rd.HasRows)
                {
                    MessageBox.Show("Existing ingredient in selected recipe!\nIf you want to modify the quantity, click the cell and Update!");
                }
                else
                {
                    Mystr = "INSERT INTO Ingredients VALUES(" + RecipeID + ",'" + IngName + "'," + Net + ",'" + Unit + "')";
                    con1.ExecuteNonQuery(Mystr);
                }
                con.Close();
                con1.Close();
            }
            else
            {
                MessageBox.Show("Ingredient Name is null!\nInput the name in order to insert!");
            }
        }

        public void UpdateIngre_Admin(int RecipeID, string IngName, int Net, string Unit)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to Update?", "Update Ingredient of Recipe!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (IngName != null)
                {
                    con.Open();
                    string st = "UPDATE Ingredients SET RecipeID = " + RecipeID + ", IngredientName = '" + IngName + "', Quantity = " + Net + ", Unit = '" + Unit + "' WHERE RecipeID = " + RecipeID + " AND IngredientName = '" + IngName + "'";
                    con.ExecuteNonQuery(st);
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Click on a Category to Update!");
                }
            }

        }

        public void DelIngre_Admin(int RecipeID, string IngName)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to Delete?", "Delete Ingredient of Recipe!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (IngName != null)
                {
                    con.Open();
                    string st = "DELETE FROM Ingredients WHERE RecipeID = " + RecipeID + " AND IngredientName = '" + IngName + "'";
                    con.ExecuteNonQuery(st);
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Ingredient Name is null!");
                }
            }

        }

        //---------------------------------------------------------ADMIN_RESEEDID-------------------------------------------------------------------


        public void ReseedID(string TableName)
        {
            DialogResult result = MessageBox.Show("Only Reseed when all the data in the table are deleted to enter new data. Do you wan to Reseed?", "Reseed ID!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                con.Open();
                string str = "DBCC CHECKIDENT('" + TableName + "', RESEED, 0)";
                con.ExecuteNonQuery(str);
                con.Close();
            }
        }

        //---------------------------------------------------------REGISTER SHOPPING LIST-------------------------------------------------------------------

        public void RegisterList()
        {
            con.Open();

            decimal ingPrice = 0;
            decimal cost = 0;
            string ingCate = null;
            string location = null;

            string str = "Select * from ShoppingList";
            SqlDataReader Rd;
            Rd = con.ExecuteReader(str);
            if (Rd.HasRows)
            {
                while (Rd.Read())
                {
                    con2.Open();
                    string myStr = "Select * from SupplyItems where IngredientName = '" + Rd["IngredientName"].ToString() + "'";
                    SqlDataReader Rdr;
                    Rdr = con2.ExecuteReader(myStr);
                    if (Rdr.HasRows)
                    {
                        while (Rdr.Read())
                        {
                            con1.Open();
                            int Temp = Convert.ToInt32(Rd["Quantity"]) + Convert.ToInt32(Rdr["Quantity"]);
                            string st = "Update SupplyItems Set Quantity = " + Temp + ", Unit = '" + Rd["Unit"].ToString() + "', DateOfArrival = '" + DateTime.Today.ToString() + "', ExpireDate = '" + DateTime.Today.AddDays(5).ToString() + "', Price = " + ingPrice + " where IngredientName = '" + Rd["IngredientName"].ToString() + "'";
                            con1.ExecuteNonQuery(st);
                            con1.Close();
                        }
                    }
                    else
                    {
                        con1.Open();
                        string st = "Insert into SupplyItems values('" + Rd["IngredientName"].ToString() + "'," + Convert.ToInt32(Rd["Quantity"]) + ",'" + Rd["Unit"].ToString() + "','" + DateTime.Today.ToString() + "','" + DateTime.Today.AddDays(5).ToString() + "'," + ingPrice + ", '" + ingCate + "')";
                        con1.ExecuteNonQuery(st);
                        con1.Close();
                    }
                    con2.Close();
                    
                    con1.Open();
                    int IngrID = 1;
                    str = "Select IngredientID from SupplyItems where IngredientName ='" + Rd["IngredientName"].ToString() + "'";
                    SqlDataReader R;
                    R = con1.ExecuteReader(str);
                    if (R.HasRows)
                    {
                        while (R.Read())
                        {
                            IngrID = Convert.ToInt32(R["IngredientID"]);

                        }
                    }
                    con1.Close();


                    con1.Open();
                    con2.Open();
                    str = "Select IngredientID from Location where IngredientID =" + IngrID;
                    SqlDataReader MyRd;
                    MyRd = con1.ExecuteReader(str);
                    if (MyRd.HasRows)
                    {
                        
                    }
                    else
                    {
                        string Mystr = "insert into Location values(" + IngrID + ",'" + location + "')";
                        con2.ExecuteNonQuery(Mystr);
                    }
                    con1.Close();
                    con2.Close();

                    //----------------------------------------------------Insert Accounting----------------------------------------------------------------------------

                    con2.Open();
                    str = "Insert into Accounting values('" + Rd["IngredientName"].ToString() + "'," + Convert.ToInt32(Rd["Quantity"]) + ",'" + Rd["Unit"].ToString() + "','" + DateTime.Today.ToString() + "','" + DateTime.Today.AddDays(5).ToString() + "'," + ingPrice + ", '" + ingCate + "'," + cost + ")";
                    con2.ExecuteNonQuery(str);
                    con2.Close();
                }
                MessageBox.Show("Insert successfully to SupplyItems and Accounting!");
            }
            else
            {
                MessageBox.Show("No data in Shopping List!");

            }
            con.Close();


        }

    }
}
