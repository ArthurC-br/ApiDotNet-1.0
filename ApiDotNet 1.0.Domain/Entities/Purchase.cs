using ApiDotNet_1._0.Domain.Entites;
using ApiDotNet_1._0.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ApiDotNet_1._0.Domain.Entities
{
    public class Purchase
    {
        public int Id { get; private set; }
        public int ProductId{ get; private set; }
        public int PersonId { get; private set; }
        public DateTime Date { get; private set; }
        public Person Person { get; set; }
        public Product Product { get; set; }
        
        public Purchase(int productId, int personId)
        {
            Validation(productId, personId);
        }
        public Purchase(int id, int productId, int personId)
        {

            DomainValidationException.When(id <= 0, "Id deve ser informado");
            Id = id;
            Validation(productId, personId);
        }
        public void Edit(int id, int productId, int personId)
        {

            DomainValidationException.When(id <= 0, "Id deve ser informado");
            Id = id;
            Validation(productId, personId);
        }
        private void Validation(int productId, int personId)
        {
            DomainValidationException.When(productId <= 0, "Id Produto deve ser informado!");
            DomainValidationException.When(personId <= 0, "Id Pessoa deve ser informado!");

            PersonId = personId;
            ProductId = productId;
            Date = DateTime.Now;

        }

    }
}
