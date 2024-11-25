using System;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.Serialization;
using Task.DB;

namespace Task.TestHelpers
{
    // Is used instead of Order class during serialization.
    public class OrderSurrogate : IDataContractSurrogate
    {
        public Type GetDataContractType(Type type)
        {
            return type;
        }

        // Maps the original object's fields to those of the surrogate.
        public object GetObjectToSerialize(object obj, Type targetType)
        {
            if (obj is Order)
            {
                var proxyOrder = (Order)obj;
                var order = InitializeOrderObject(proxyOrder);
                return order;
            }
            else if (obj is Customer)
            {
                var proxyCustomer = (Customer)obj;
                var customer = InitializeCustomerObject(proxyCustomer);
                return customer;
            }

            else if (obj is Employee)
            {
                var proxyEmployee = (Employee)obj;
                var employee = InitializeEmployeeObject(proxyEmployee);
                return employee;
            }
            else if (obj is Order_Detail)
            {
                var proxyOrderDetail = (Order_Detail)obj;
                var orderDetail = InitializeOrderDetailObject(proxyOrderDetail);
                return orderDetail;
            }
            else if (obj is Shipper)
            {
                var ship = (Shipper)obj;
                var shipper = new Shipper()
                {
                    CompanyName = ship.CompanyName,
                    Phone = ship.Phone,
                    ShipperID = ship.ShipperID
                };
                return shipper;
            }

            return obj;
        }

        public object GetDeserializedObject(object obj, Type targetType)
        {
            if (obj is Order)
            {
                Order proxyOrder = (Order)obj;
                var order = InitializeOrderObject(proxyOrder);
                return order;
            }
            return obj;
        }

        private Order InitializeOrderObject(Order proxyOrder)
        {
            var order = new Order()
            {
                OrderID = proxyOrder.OrderID,
                CustomerID = proxyOrder.CustomerID,
                EmployeeID = proxyOrder.EmployeeID,
                OrderDate = proxyOrder.OrderDate,
                RequiredDate = proxyOrder.RequiredDate,
                ShippedDate = proxyOrder.ShippedDate,
                ShipVia = proxyOrder.ShipVia,
                Freight = proxyOrder.Freight,
                ShipName = proxyOrder.ShipName,
                ShipAddress = proxyOrder.ShipAddress,
                ShipCity = proxyOrder.ShipCity,
                ShipRegion = proxyOrder.ShipRegion,
                ShipPostalCode = proxyOrder.ShipPostalCode,
                ShipCountry = proxyOrder.ShipCountry,
                Customer = proxyOrder.Customer,
                Employee = proxyOrder.Employee,
                Order_Details = proxyOrder.Order_Details,
                Shipper = proxyOrder.Shipper
            };
            return order;
        }

        private Customer InitializeCustomerObject(Customer proxyCustomer)
        {
            Customer customer = new Customer()
            {
                CustomerID = proxyCustomer.CustomerID,
                CompanyName = proxyCustomer.CompanyName,
                ContactName = proxyCustomer.ContactName,
                ContactTitle = proxyCustomer.ContactTitle,
                Address = proxyCustomer.Address,
                City = proxyCustomer.City,
                Region = proxyCustomer.Region,
                PostalCode = proxyCustomer.PostalCode,
                Country = proxyCustomer.Country,
                Phone = proxyCustomer.Phone,
                Fax = proxyCustomer.Fax
            };
            return customer;
        }

        private Employee InitializeEmployeeObject(Employee proxyEmployee)
        {
            var employee = new Employee()
            {
                EmployeeID = proxyEmployee.EmployeeID,
                Address = proxyEmployee.Address,
                BirthDate = proxyEmployee.BirthDate,
                City = proxyEmployee.City,
                Country = proxyEmployee.Country,
                PostalCode = proxyEmployee.PostalCode,
                HomePhone = proxyEmployee.HomePhone,
                Extension = proxyEmployee.Extension,
                FirstName = proxyEmployee.FirstName,
                HireDate = proxyEmployee.HireDate,
                LastName = proxyEmployee.LastName,
                Notes = proxyEmployee.Notes,
                Photo = proxyEmployee.Photo,
                PhotoPath = proxyEmployee.PhotoPath,
                ReportsTo = proxyEmployee.ReportsTo,
                Title = proxyEmployee.Title,
                TitleOfCourtesy = proxyEmployee.TitleOfCourtesy,
                Region = proxyEmployee.Region
            };
            return employee;
        }

        private Order_Detail InitializeOrderDetailObject(Order_Detail proxyOrderDetail)
        {
            var orderDetail = new Order_Detail()
            {
                Discount = proxyOrderDetail.Discount,
                OrderID = proxyOrderDetail.OrderID,
                ProductID = proxyOrderDetail.ProductID,
                Quantity = proxyOrderDetail.Quantity,
                UnitPrice = proxyOrderDetail.UnitPrice
            };
            return orderDetail;
        }

        public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public object GetCustomDataToExport(Type clrType, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
        {
            throw new NotImplementedException();
        }

        public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
        {
            throw new NotImplementedException();
        }

        public CodeTypeDeclaration ProcessImportedType(CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit)
        {
            throw new NotImplementedException();
        }
    }
}
