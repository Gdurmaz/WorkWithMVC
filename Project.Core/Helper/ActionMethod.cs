using System;
using System.Data.Entity.Validation;
namespace Project.Core.Helper
{
    public class ActionMethod
    {
        public static void DbEntityException(Action action)
        {
            try
            {
                action();
            }
            catch (DbEntityValidationException deve)
            {
                foreach (var eve in deve.EntityValidationErrors)
                {
                    //Method için log tabanı oluşturulacak
                    Console.WriteLine($"Girdi: {eve.Entry} - Model:{eve.IsValid} ");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine($"Property Adı: {ve.PropertyName} - Hata Mesajı:{ve.ErrorMessage} ");
                    }
                }
            }
        }
        public static void EMailException(Action action)
        {
            try
            {
                action();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
