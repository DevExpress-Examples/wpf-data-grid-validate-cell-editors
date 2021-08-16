using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Xpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ValidateCell_MVVM {
    public class Product {
        public string ProductName { get; set; }
        public double UnitPrice { get; set; }
        public bool Discontinued { get; set; }

        public Product(int i) {
            ProductName = "Product" + i;
            Discontinued = i % 5 == 0;
        }
    }
    public class MainViewModel : ViewModelBase {
        public ObservableCollection<Product> ProductList { get; }

        public MainViewModel() {
            ProductList = new ObservableCollection<Product>(GetProductList());
        }
        static IEnumerable<Product> GetProductList() {
            Random r = new Random();
            return Enumerable.Range(0, 20).Select(i => new Product(i) { UnitPrice = r.Next(1, 50) });
        }

        [Command]
        public void ValidateCell(RowCellValidationArgs args) {
            if(args.FieldName != nameof(Product.UnitPrice) || !((Product)args.Item).Discontinued)
                return;
            var cellValue = (double)args.OldValue;
            var discount = 100 - Convert.ToDouble(args.NewValue) / cellValue * 100;
            if(discount > 0 && discount <= 30)
                return;

            var error = discount < 0
                ? $"The price cannot be greater than {cellValue}"
                : $"The discount cannot be greater than 30% ({cellValue * 0.7}). Please correct the price.";
            args.Result = new ValidationErrorInfo(error, ValidationErrorType.Critical);
        }
    }
}
