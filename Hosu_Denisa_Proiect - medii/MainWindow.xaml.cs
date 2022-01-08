using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GymModel1;
using System.Data.Entity;
using System.Data;
using Hosu_Denisa_Proiect;

namespace Hosu_Denisa_Proiect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    enum ActionState
    {
        New,
        Edit,
        Delete,
        Nothing
    }
    public partial class MainWindow : Window
    {
        ActionState action = ActionState.Nothing;
       GymEntitiesModel1 ctx = new GymEntitiesModel1();

        CollectionViewSource customerViewSource;
        Binding txtnumeBinding = new Binding();
        Binding txtprenumeBinding = new Binding();
        Binding txtcodabonamenteBinding = new Binding();

        CollectionViewSource productViewSource;
        Binding txtnume1Binding = new Binding();
        Binding txtprenume1Binding = new Binding();
        Binding txtziuaBinding = new Binding();
        Binding txtoraBinding = new Binding();
        Binding txtidCsBinding = new Binding();


        CollectionViewSource customerOrdersViewSource;
        Binding txtCustomer = new Binding();
        Binding txtProduct = new Binding();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            txtnumeBinding.Path = new PropertyPath("Nume");
            txtprenumeBinding.Path = new PropertyPath("Prenume");
            txtcodabonamenteBinding.Path = new PropertyPath("CodAbonamente");
            txtnume1Binding.Path = new PropertyPath("Nume1");
            txtprenume1Binding.Path = new PropertyPath("Prenume1");
            txtziuaBinding.Path = new PropertyPath("Ziua");
            txtoraBinding.Path = new PropertyPath("Ora");
            txtidCsBinding.Path = new PropertyPath("IdCs");
            txtCustomer.Path = new PropertyPath("Customer");
            txtProduct.Path = new PropertyPath("Product");

            numeTextBox.SetBinding(TextBox.TextProperty, txtnumeBinding);
            prenumeTextBox.SetBinding(TextBox.TextProperty, txtprenumeBinding);
            codabonamenteTextBox.SetBinding(TextBox.TextProperty, txtcodabonamenteBinding);
  
            nume1TextBox.SetBinding(TextBox.TextProperty, txtnume1Binding);
            prenume1TextBox.SetBinding(TextBox.TextProperty, txtprenume1Binding);
            ziuaTextBox.SetBinding(TextBox.TextProperty, txtziuaBinding);
            oraTextBox.SetBinding(TextBox.TextProperty, txtoraBinding);
            idCsTextBox.SetBinding(TextBox.TextProperty, txtidCsBinding);
            cmbCustomer.SetBinding(ComboBox.TextProperty, txtCustomer);
            cmbProduct.SetBinding(ComboBox.TextProperty, txtProduct);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            customerViewSource =
                ((System.Windows.Data.CollectionViewSource)(this.FindResource("customerViewSource")));
            customerViewSource.Source = ctx.Customers.Local;
            ctx.Customers.Load();

            customerOrdersViewSource =
                ((System.Windows.Data.CollectionViewSource)(this.FindResource("customerOrdersViewSource")));
            //customerOrdersViewSource.Source = ctx.Orders.Local;
            ctx.Orders.Load();

            productViewSource =
                ((System.Windows.Data.CollectionViewSource)(this.FindResource("productViewSource")));
            productViewSource.Source = ctx.Products.Local;
            ctx.Products.Load();

            cmbCustomer.ItemsSource = ctx.Customers.Local;
            cmbCustomer.SelectedValuePath = "IdClient";

            cmbProduct.ItemsSource = ctx.Products.Local;
            cmbProduct.SelectedValuePath = "IdAntrenori";

            BindDataGrid();
        }

        private void btnNewC_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            btnNewC.IsEnabled = false;
            btnEditC.IsEnabled = false;
            btnDeleteC.IsEnabled = false;

            btnSaveC.IsEnabled = true;
            btnCancelC.IsEnabled = true;
            customerDataGrid.IsEnabled = false;
            btnPrevC.IsEnabled = false;
            btnNextC.IsEnabled = false;

             numeTextBox.IsEnabled = true;
            prenumeTextBox.IsEnabled = true;
            codabonamenteTextBox.IsEnabled = true;

            BindingOperations.ClearBinding(numeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(prenumeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(codabonamenteTextBox, TextBox.TextProperty);
            numeTextBox.Text = "";
            prenumeTextBox.Text = "";
            codabonamenteTextBox.Text = "";
            Keyboard.Focus(numeTextBox);
        }

        private void btnEditC_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;

            string tempFirstName = numeTextBox.Text.ToString();
            string tempLastName = prenumeTextBox.Text.ToString();
            string tempAge = codabonamenteTextBox.Text.ToString();

            btnNewC.IsEnabled = false;
            btnEditC.IsEnabled = false;
            btnDeleteC.IsEnabled = false;

            btnSaveC.IsEnabled = true;
            btnCancelC.IsEnabled = true;
            customerDataGrid.IsEnabled = false;
            btnPrevC.IsEnabled = false;
            btnNextC.IsEnabled = false;

            numeTextBox.IsEnabled = true;
            prenumeTextBox.IsEnabled = true;
            codabonamenteTextBox.IsEnabled = true;

            BindingOperations.ClearBinding(numeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(prenumeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(codabonamenteTextBox, TextBox.TextProperty);
            numeTextBox.Text = tempFirstName;
            prenumeTextBox.Text = tempLastName;
            codabonamenteTextBox.Text = tempAge;
            Keyboard.Focus(numeTextBox);

            SetValidationBinding();
        }

        private void btnDeleteC_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;

            string tempNume = numeTextBox.Text.ToString();
            string tempPrenume = prenumeTextBox.Text.ToString();
            string tempCodabonamente = codabonamenteTextBox.Text.ToString();

            btnNewC.IsEnabled = false;
            btnEditC.IsEnabled = false;
            btnDeleteC.IsEnabled = false;

            btnSaveC.IsEnabled = true;
            btnCancelC.IsEnabled = true;
            customerDataGrid.IsEnabled = false;
            btnPrevC.IsEnabled = false;
            btnNextC.IsEnabled = false;

            BindingOperations.ClearBinding(numeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(prenumeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(codabonamenteTextBox, TextBox.TextProperty);
            numeTextBox.Text = tempNume;
            prenumeTextBox.Text = tempPrenume;
            codabonamenteTextBox.Text = tempCodabonamente;
        }

        private void btnSaveC_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = null;
            if (action == ActionState.New)
            {
                try
                {
                    customer = new Customer()
                    {
                        Nume = numeTextBox.Text.Trim(),
                        Prenume = prenumeTextBox.Text.Trim(),
                        CodAbonamente = codabonamenteTextBox.Text.Trim()
                    };
                    ctx.Customers.Add(customer);
                    customerViewSource.View.Refresh();
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                btnNewC.IsEnabled = true;
                btnEditC.IsEnabled = true;
                btnDeleteC.IsEnabled = true;
                btnSaveC.IsEnabled = false;
                btnCancelC.IsEnabled = false;
                customerDataGrid.IsEnabled = true;
                btnPrevC.IsEnabled = true;
                btnNextC.IsEnabled = true;
                numeTextBox.IsEnabled = false;
                prenumeTextBox.IsEnabled = false;
                codabonamenteTextBox.IsEnabled = false;
            }
            else if (action == ActionState.Edit)
            {
                try
                {
                    customer = (Customer)customerDataGrid.SelectedItem;
                    customer.Nume = numeTextBox.Text.Trim();
                    customer.Prenume = prenumeTextBox.Text.Trim();
                    customer.CodAbonamente = codabonamenteTextBox.Text.Trim();
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                customerViewSource.View.Refresh();
                customerViewSource.View.MoveCurrentTo(customer);
                btnNewC.IsEnabled = true;
                btnEditC.IsEnabled = true;
                btnDeleteC.IsEnabled = true;
                btnSaveC.IsEnabled = false;
                btnCancelC.IsEnabled = false;
                customerDataGrid.IsEnabled = true;
                btnPrevC.IsEnabled = true;
                btnNextC.IsEnabled = true;
                numeTextBox.IsEnabled = false;
                prenumeTextBox.IsEnabled = false;
                codabonamenteTextBox.IsEnabled = false;
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    customer = (Customer)customerDataGrid.SelectedItem;
                    ctx.Customers.Remove(customer);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                customerViewSource.View.Refresh();
                customerViewSource.View.MoveCurrentTo(customer);
                btnNewC.IsEnabled = true;
                btnEditC.IsEnabled = true;
                btnDeleteC.IsEnabled = true;
                btnSaveC.IsEnabled = false;
                btnCancelC.IsEnabled = false;
                customerDataGrid.IsEnabled = true;
                btnPrevC.IsEnabled = true;
                btnNextC.IsEnabled = true;
                numeTextBox.IsEnabled = false;
               prenumeTextBox.IsEnabled = false;
                codabonamenteTextBox.IsEnabled = false;

                numeTextBox.SetBinding(TextBox.TextProperty, txtnumeBinding);
                prenumeTextBox.SetBinding(TextBox.TextProperty, txtprenumeBinding);
                codabonamenteTextBox.SetBinding(TextBox.TextProperty, txtcodabonamenteBinding);
            }
            SetValidationBinding();
        }

        private void btnCancelC_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Nothing;
            btnNewC.IsEnabled = true;
            btnEditC.IsEnabled = true;
            btnDeleteC.IsEnabled = true;
            btnSaveC.IsEnabled = false;
            btnCancelC.IsEnabled = false;
            customerDataGrid.IsEnabled = true;
            btnPrevC.IsEnabled = true;
            btnNextC.IsEnabled = true;

            numeTextBox.IsEnabled = false;
            prenumeTextBox.IsEnabled = false;
            codabonamenteTextBox.IsEnabled = false;

            numeTextBox.SetBinding(TextBox.TextProperty, txtnumeBinding);
            prenumeTextBox.SetBinding(TextBox.TextProperty, txtprenumeBinding);
            codabonamenteTextBox.SetBinding(TextBox.TextProperty, txtcodabonamenteBinding);
        }

        private void btnPrevC_Click(object sender, RoutedEventArgs e)
        {
            customerViewSource.View.MoveCurrentToPrevious();
        }

        private void btnNextC_Click(object sender, RoutedEventArgs e)
        {
            customerViewSource.View.MoveCurrentToNext();
        }

        private void btnNewP_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            btnNewP.IsEnabled = false;
            btnEditP.IsEnabled = false;
            btnDeleteP.IsEnabled = false;

            btnSaveP.IsEnabled = true;
            btnCancelP.IsEnabled = true;
            productDataGrid.IsEnabled = false;
            btnPrevP.IsEnabled = false;
            btnNextP.IsEnabled = false;

           nume1TextBox.IsEnabled = true;
           prenume1TextBox.IsEnabled = true;
           ziuaTextBox.IsEnabled = true;
            oraTextBox.IsEnabled = true;
            idCsTextBox.IsEnabled = true;

            BindingOperations.ClearBinding(nume1TextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(prenume1TextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(ziuaTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(oraTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(idCsTextBox, TextBox.TextProperty);
            nume1TextBox.Text = "";
            prenume1TextBox.Text = "";
           ziuaTextBox.Text = "";
            oraTextBox.Text = "";
            idCsTextBox.Text = "";
            Keyboard.Focus(nume1TextBox);
        }

        private void btnEditP_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;

            string tempNume1 = nume1TextBox.Text.ToString();
            string tempPrenume1 = prenume1TextBox.Text.ToString();
            string tempZiua = ziuaTextBox.Text.ToString();
            string tempOra = oraTextBox.Text.ToString();
            string tempidCs = idCsTextBox.Text.ToString();

            btnNewP.IsEnabled = false;
            btnEditP.IsEnabled = false;
            btnDeleteP.IsEnabled = false;

            btnSaveP.IsEnabled = true;
            btnCancelP.IsEnabled = true;
            productDataGrid.IsEnabled = false;
            btnPrevP.IsEnabled = false;
            btnNextP.IsEnabled = false;

            nume1TextBox.IsEnabled = true;
            prenume1TextBox.IsEnabled = true;
            ziuaTextBox.IsEnabled = true;
            oraTextBox.IsEnabled = true;
            idCsTextBox.IsEnabled = true;

            BindingOperations.ClearBinding(nume1TextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(prenume1TextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(ziuaTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(oraTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(idCsTextBox, TextBox.TextProperty);
            nume1TextBox.Text = tempNume1;
            prenume1TextBox.Text = tempPrenume1;
            ziuaTextBox.Text = tempZiua;
            oraTextBox.Text = tempOra;
            idCsTextBox.Text = tempidCs;
            Keyboard.Focus(nume1TextBox);

            SetValidationBinding();
        }

        private void btnDeleteP_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;

            string tempNume1 = nume1TextBox.Text.ToString();
            string tempPrenume1 = prenume1TextBox.Text.ToString();
            string tempZiua = ziuaTextBox.Text.ToString();
            string tempOra = oraTextBox.Text.ToString();
            string tempidCs = idCsTextBox.Text.ToString();

            btnNewP.IsEnabled = false;
            btnEditP.IsEnabled = false;
            btnDeleteP.IsEnabled = false;

            btnSaveP.IsEnabled = true;
            btnCancelP.IsEnabled = true;
            productDataGrid.IsEnabled = false;
            btnPrevP.IsEnabled = false;
            btnNextP.IsEnabled = false;


            BindingOperations.ClearBinding(nume1TextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(prenume1TextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(ziuaTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(oraTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(idCsTextBox, TextBox.TextProperty);
            nume1TextBox.Text = tempNume1;
            prenume1TextBox.Text = tempPrenume1;
            ziuaTextBox.Text = tempZiua;
            oraTextBox.Text = tempOra;
            idCsTextBox.Text = tempidCs;
            Keyboard.Focus(nume1TextBox);
        }

        private void btnSaveP_Click(object sender, RoutedEventArgs e)
        {
            Product product = null;
            if (action == ActionState.New)
            {
                try
                {
                    product = new Product()
                    {
                       Nume1 = nume1TextBox.Text.Trim(),
                       Prenume1 = prenume1TextBox.Text.Trim(),
                        Ziua = ziuaTextBox.Text.Trim(),
                        Ora = oraTextBox.Text.Trim(),
                        IdCs = idCsTextBox.Text.Trim(),
                    };
                    ctx.Products.Add(product);
                    productViewSource.View.Refresh();
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                btnNewP.IsEnabled = true;
                btnEditP.IsEnabled = true;
                btnDeleteP.IsEnabled = true;
                btnSaveP.IsEnabled = false;
                btnCancelP.IsEnabled = false;
                productDataGrid.IsEnabled = true;
                btnPrevP.IsEnabled = true;
                btnNextP.IsEnabled = true;
                nume1TextBox.IsEnabled = false;
                prenume1TextBox.IsEnabled = false;
               ziuaTextBox.IsEnabled = false;
                oraTextBox.IsEnabled = false;
                idCsTextBox.IsEnabled = false;
            }
            else if (action == ActionState.Edit)
            {
                try
                {
                    product = (Product)productDataGrid.SelectedItem;
                    product.Nume1 = nume1TextBox.Text.Trim();
                    product.Prenume1 = prenume1TextBox.Text.Trim();
                    product.Ziua = ziuaTextBox.Text.Trim();
                    product.Ora = oraTextBox.Text.Trim();
                    product.IdCs = idCsTextBox.Text.Trim();
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                productViewSource.View.Refresh();
                productViewSource.View.MoveCurrentTo(product);
                btnNewP.IsEnabled = true;
                btnEditP.IsEnabled = true;
                btnDeleteP.IsEnabled = true;
                btnSaveP.IsEnabled = false;
                btnCancelP.IsEnabled = false;
                productDataGrid.IsEnabled = true;
                btnPrevP.IsEnabled = true;
                btnNextP.IsEnabled = true;
                nume1TextBox.IsEnabled = false;
                prenume1TextBox.IsEnabled = false;
                ziuaTextBox.IsEnabled = false;
                oraTextBox.IsEnabled = false;
                idCsTextBox.IsEnabled = false;
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    product = (Product)productDataGrid.SelectedItem;
                    ctx.Products.Remove(product);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                productViewSource.View.Refresh();
                productViewSource.View.MoveCurrentTo(product);
                btnNewP.IsEnabled = true;
                btnEditP.IsEnabled = true;
                btnDeleteP.IsEnabled = true;
                btnSaveP.IsEnabled = false;
                btnCancelP.IsEnabled = false;
                productDataGrid.IsEnabled = true;
                btnPrevP.IsEnabled = true;
                btnNextP.IsEnabled = true;
                nume1TextBox.IsEnabled = false;
                prenume1TextBox.IsEnabled = false;
                ziuaTextBox.IsEnabled = false;
                oraTextBox.IsEnabled = false;
                idCsTextBox.IsEnabled = false;

               nume1TextBox.SetBinding(TextBox.TextProperty, txtnume1Binding);
                prenume1TextBox.SetBinding(TextBox.TextProperty, txtprenume1Binding);
              ziuaTextBox.SetBinding(TextBox.TextProperty, txtziuaBinding);
                oraTextBox.SetBinding(TextBox.TextProperty, txtoraBinding);
                idCsTextBox.SetBinding(TextBox.TextProperty, txtidCsBinding);
            }
            SetValidationBinding();
        }

        private void btnCancelP_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Nothing;
            btnNewP.IsEnabled = true;
            btnEditP.IsEnabled = true;
            btnDeleteP.IsEnabled = true;
            btnSaveP.IsEnabled = false;
            btnCancelP.IsEnabled = false;
            productDataGrid.IsEnabled = true;
            btnPrevP.IsEnabled = true;
            btnNextP.IsEnabled = true;

            nume1TextBox.IsEnabled = false;
            prenume1TextBox.IsEnabled = false;
            ziuaTextBox.IsEnabled = false;
            oraTextBox.IsEnabled = false;
            idCsTextBox.IsEnabled = false;

            nume1TextBox.SetBinding(TextBox.TextProperty, txtnume1Binding);
            prenume1TextBox.SetBinding(TextBox.TextProperty, txtprenume1Binding);
            ziuaTextBox.SetBinding(TextBox.TextProperty, txtziuaBinding);
            oraTextBox.SetBinding(TextBox.TextProperty, txtoraBinding);
            idCsTextBox.SetBinding(TextBox.TextProperty, txtidCsBinding);
        }

        private void btnPrevP_Click(object sender, RoutedEventArgs e)
        {
            productViewSource.View.MoveCurrentToPrevious();
        }

        private void btnNextP_Click(object sender, RoutedEventArgs e)
        {
            productViewSource.View.MoveCurrentToNext();
        }

        private void btnNewO_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            btnNewO.IsEnabled = false;
            btnEditO.IsEnabled = false;
            btnDeleteO.IsEnabled = false;

            btnSaveO.IsEnabled = true;
            btnCancelO.IsEnabled = true;
            ordersDataGrid.IsEnabled = false;
            btnPrevO.IsEnabled = false;
            btnNextO.IsEnabled = false;

            cmbCustomer.IsEnabled = true;
            cmbProduct.IsEnabled = true;

            BindingOperations.ClearBinding(cmbCustomer, ComboBox.TextProperty);
            BindingOperations.ClearBinding(cmbProduct, ComboBox.TextProperty);
            cmbCustomer.Text = "";
            cmbProduct.Text = "";
            Keyboard.Focus(cmbCustomer);
        }

        private void btnEditO_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;

            string tempCustomer = cmbCustomer.Text.ToString();
            string tempProduct = cmbProduct.Text.ToString();

            btnNewO.IsEnabled = false;
            btnEditO.IsEnabled = false;
            btnDeleteO.IsEnabled = false;

            btnSaveO.IsEnabled = true;
            btnCancelO.IsEnabled = true;
            ordersDataGrid.IsEnabled = false;
            btnPrevO.IsEnabled = false;
            btnNextO.IsEnabled = false;

            cmbCustomer.IsEnabled = true;
            cmbProduct.IsEnabled = true;

            BindingOperations.ClearBinding(cmbCustomer, ComboBox.TextProperty);
            BindingOperations.ClearBinding(cmbProduct, ComboBox.TextProperty);
            cmbCustomer.Text = tempCustomer;
            cmbProduct.Text = tempProduct;
            Keyboard.Focus(cmbCustomer);

            SetValidationBinding();
        }

        private void btnDeleteO_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;

            string tempCustomer = cmbCustomer.Text.ToString();
            string tempProduct = cmbProduct.Text.ToString();

            btnNewO.IsEnabled = false;
            btnEditO.IsEnabled = false;
            btnDeleteO.IsEnabled = false;

            btnSaveO.IsEnabled = true;
            btnCancelO.IsEnabled = true;
            ordersDataGrid.IsEnabled = false;
            btnPrevO.IsEnabled = false;
            btnNextO.IsEnabled = false;

            BindingOperations.ClearBinding(cmbCustomer, TextBox.TextProperty);
            BindingOperations.ClearBinding(cmbProduct, TextBox.TextProperty);
            cmbCustomer.Text = tempCustomer;
            cmbProduct.Text = tempProduct;
        }

        private void btnSaveO_Click(object sender, RoutedEventArgs e)
        {
            Order order = null;
            if (action == ActionState.New)
            {
                try
                {
                    Customer customers = (Customer)cmbCustomer.SelectedItem;
                    Product products = (Product)cmbProduct.SelectedItem;

                    order = new Order()

                    {
                        IdClient = customers.IdClient,
                        IdAntrenori = products.IdAntrenori
                    };
                    ctx.Orders.Add(order);
                    customerOrdersViewSource.View.Refresh();
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                btnNewO.IsEnabled = true;
                btnEditO.IsEnabled = true;
                btnDeleteO.IsEnabled = true;
                btnSaveO.IsEnabled = false;
                btnCancelO.IsEnabled = false;
                ordersDataGrid.IsEnabled = true;
                btnPrevO.IsEnabled = true;
                btnNextO.IsEnabled = true;
                cmbCustomer.IsEnabled = false;
                cmbProduct.IsEnabled = false;
            }
            else if (action == ActionState.Edit)
            {
                dynamic selectedOrder = ordersDataGrid.SelectedItem;
                try
                {
                    int curr_id = selectedOrder.IdProgramare;

                    var editedOrder = ctx.Orders.FirstOrDefault(s => s.IdProgramare == curr_id);
                    if (editedOrder != null)
                    {
                        editedOrder.IdClient = Int32.Parse(cmbCustomer.SelectedValue.ToString());
                        editedOrder.IdAntrenori = Int32.Parse(cmbProduct.SelectedValue.ToString());
                        ctx.SaveChanges();
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                BindDataGrid();
                customerViewSource.View.MoveCurrentTo(selectedOrder);

                customerOrdersViewSource.View.Refresh();
                customerOrdersViewSource.View.MoveCurrentTo(order);
                btnNewO.IsEnabled = true;
                btnEditO.IsEnabled = true;
                btnDeleteO.IsEnabled = true;
                btnSaveO.IsEnabled = false;
                btnCancelO.IsEnabled = false;
                ordersDataGrid.IsEnabled = true;
                btnPrevO.IsEnabled = true;
                btnNextO.IsEnabled = true;
                cmbCustomer.IsEnabled = false;
                cmbProduct.IsEnabled = false;
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    dynamic selectedOrder = ordersDataGrid.SelectedItem;

                    int curr_id = selectedOrder.IdProgramare;
                    var deletedOrder = ctx.Orders.FirstOrDefault(s => s.IdProgramare == curr_id);
                    if (deletedOrder != null)
                    {
                        ctx.Orders.Remove(deletedOrder);
                        ctx.SaveChanges();
                        MessageBox.Show("Order Deleted Sucessfully", "Message");
                        BindDataGrid();
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                customerOrdersViewSource.View.Refresh();
                customerOrdersViewSource.View.MoveCurrentTo(order);
                btnNewO.IsEnabled = true;
                btnEditO.IsEnabled = true;
                btnDeleteO.IsEnabled = true;
                btnSaveO.IsEnabled = false;
                btnCancelO.IsEnabled = false;
                ordersDataGrid.IsEnabled = true;
                btnPrevO.IsEnabled = true;
                btnNextO.IsEnabled = true;
                cmbCustomer.IsEnabled = false;
                cmbProduct.IsEnabled = false;

                cmbCustomer.SetBinding(ComboBox.TextProperty, txtCustomer);
                cmbProduct.SetBinding(ComboBox.TextProperty, txtProduct);
            }
            SetValidationBinding();
        }

        private void btnCancelO_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Nothing;
            btnNewO.IsEnabled = true;
            btnEditO.IsEnabled = true;
            btnDeleteO.IsEnabled = true;
            btnSaveO.IsEnabled = false;
            btnCancelO.IsEnabled = false;
            ordersDataGrid.IsEnabled = true;
            btnPrevO.IsEnabled = true;
            btnNextO.IsEnabled = true;

            cmbCustomer.IsEnabled = false;
            cmbProduct.IsEnabled = false;

            cmbCustomer.SetBinding(ComboBox.TextProperty, txtCustomer);
            cmbProduct.SetBinding(ComboBox.TextProperty, txtProduct);
        }

        private void btnPrevO_Click(object sender, RoutedEventArgs e)
        {
            customerOrdersViewSource.View.MoveCurrentToPrevious();
        }

        private void btnNextO_Click(object sender, RoutedEventArgs e)
        {
            customerOrdersViewSource.View.MoveCurrentToNext();
        }

        private void BindDataGrid()
        {
            var queryOrder = from ord in ctx.Orders
                             join cust in ctx.Customers on ord.IdClient equals cust.IdClient
                             join prod in ctx.Products on ord.IdAntrenori equals prod.IdAntrenori
                             select new { ord.IdProgramare, ord.IdAntrenori, ord.IdClient, cust.Nume, cust.Prenume, cust.CodAbonamente, prod.Nume1, prod.Prenume1, prod.Ziua, prod.Ora };
            customerOrdersViewSource.Source = queryOrder.ToList();
        }

        private void SetValidationBinding()
        {
            Binding IdAntrenoriValidationBinding = new Binding();
            IdAntrenoriValidationBinding.Source = customerViewSource;
            IdAntrenoriValidationBinding.Path = new PropertyPath("IdAntrenori");
            IdAntrenoriValidationBinding.NotifyOnValidationError = true;
            IdAntrenoriValidationBinding.Mode = BindingMode.TwoWay;
            IdAntrenoriValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            IdAntrenoriValidationBinding.ValidationRules.Add(new StringNotEmpty());
            idAntrenoriTextBox.SetBinding(TextBox.TextProperty, IdAntrenoriValidationBinding);

            Binding IdClientValidationBinding = new Binding();
            IdClientValidationBinding.Source = customerViewSource;
            IdClientValidationBinding.Path = new PropertyPath("IdClient");
            IdClientValidationBinding.NotifyOnValidationError = true;
            IdClientValidationBinding.Mode = BindingMode.TwoWay;
            IdClientValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            IdClientValidationBinding.ValidationRules.Add(new StringNotEmpty());
            idClientTextBox.SetBinding(TextBox.TextProperty, IdClientValidationBinding);


            Binding numeValidationBinding = new Binding();
            numeValidationBinding.Source = customerViewSource;
            numeValidationBinding.Path = new PropertyPath("Nume");
            numeValidationBinding.NotifyOnValidationError = true;
            numeValidationBinding.Mode = BindingMode.TwoWay;
            numeValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            numeValidationBinding.ValidationRules.Add(new StringNotEmpty());
           numeTextBox.SetBinding(TextBox.TextProperty, numeValidationBinding);

            Binding prenumeValidationBinding = new Binding();
            prenumeValidationBinding.Source = customerViewSource;
            prenumeValidationBinding.Path = new PropertyPath("Prenume");
            prenumeValidationBinding.NotifyOnValidationError = true;
            prenumeValidationBinding.Mode = BindingMode.TwoWay;
            prenumeValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            prenumeValidationBinding.ValidationRules.Add(new StringNotEmpty());
            prenumeTextBox.SetBinding(TextBox.TextProperty, prenumeValidationBinding);

            Binding codabonamenteValidationBinding = new Binding();
            codabonamenteValidationBinding.Source = customerViewSource;
            codabonamenteValidationBinding.Path = new PropertyPath("CodAbonamente");
            codabonamenteValidationBinding.NotifyOnValidationError = true;
            codabonamenteValidationBinding.Mode = BindingMode.TwoWay;
            codabonamenteValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            codabonamenteValidationBinding.ValidationRules.Add(new StringNotEmpty());
           codabonamenteTextBox.SetBinding(TextBox.TextProperty, codabonamenteValidationBinding);

            Binding nume1ValidationBinding = new Binding();
            nume1ValidationBinding.Source = productViewSource;
            nume1ValidationBinding.Path = new PropertyPath("Nume1");
            nume1ValidationBinding.NotifyOnValidationError = true;
            nume1ValidationBinding.Mode = BindingMode.TwoWay;
            nume1ValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            nume1ValidationBinding.ValidationRules.Add(new StringNotEmpty());
            nume1TextBox.SetBinding(TextBox.TextProperty, nume1ValidationBinding);


            Binding ziuaValidationBinding = new Binding();
            ziuaValidationBinding.Source = productViewSource;
            ziuaValidationBinding.Path = new PropertyPath("Ziua");
            ziuaValidationBinding.NotifyOnValidationError = true;
            ziuaValidationBinding.Mode = BindingMode.TwoWay;
            ziuaValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            ziuaValidationBinding.ValidationRules.Add(new StringNotEmpty());
            ziuaTextBox.SetBinding(TextBox.TextProperty, ziuaValidationBinding);

            Binding oraValidationBinding = new Binding();
            oraValidationBinding.Source = productViewSource;
            oraValidationBinding.Path = new PropertyPath("Ora");
            oraValidationBinding.NotifyOnValidationError = true;
            oraValidationBinding.Mode = BindingMode.TwoWay;
            oraValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            ziuaValidationBinding.ValidationRules.Add(new StringNotEmpty());
            oraTextBox.SetBinding(TextBox.TextProperty, oraValidationBinding);
        }

     
    }
}
