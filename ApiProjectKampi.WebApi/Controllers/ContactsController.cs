using ApiProjectKampi.WebApi.Context;
using ApiProjectKampi.WebApi.Dtos.ContactDtos;
using ApiProjectKampi.WebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjectKampi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ApiContext _context;
        public ContactsController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ContactList()
        {
            var values= _context.Contacts.ToList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateContact(CreateContactDto createContactDto)
        {
            Contact contact = new Contact();
            contact.Email= createContactDto.Email;
            contact.Address= createContactDto.Address;
            contact.Phone= createContactDto.Phone;
            contact.MapLocation= createContactDto.MapLocation;
            contact.OpenHours= createContactDto.OpenHours;
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return Ok("Ekleme islemi basarili");
        }

        [HttpDelete]
        public IActionResult DeleteContact(int id)
        {
            var value = _context.Contacts.Find(id);
            _context.Contacts.Remove(value);
            _context.SaveChanges();
            return Ok("Silme islemi basarili"); 

        }


        [HttpGet("GetContact")]
        public IActionResult GetContact(int id)
        {
            var value = _context.Contacts.Find(id);
            return Ok(value);
        }



        //[HttpPut]
        //public IActionResult UpdateContact(UpdateContactDto updateContactDto)
        //{
        //    Contact value = new Contact();
        //    value.Email = updateContactDto.Email;
        //    value.Address = updateContactDto.Address;
        //    value.Phone = updateContactDto.Phone;
        //    value.MapLocation = updateContactDto.MapLocation;
        //    value.OpenHours = updateContactDto.OpenHours;
        //    _context.Contacts.Update(value);
        //    _context.SaveChanges();
        //    return Ok("Guncelleme islemi basarili");
        //}

        [HttpPut("{id}")]
        public IActionResult UpdateContact(int id, UpdateContactDto updateContactDto)
        {
            // Veritabanında ID'ye göre ilgili kaydı bul
            var contact = _context.Contacts.Find(id);

            if (contact == null)
            {
                return NotFound("Belirtilen ID'ye sahip veri bulunamadı.");
            }

            // Güncellenmesi gereken alanları güncelle
            contact.Email = updateContactDto.Email;
            contact.Address = updateContactDto.Address;
            contact.Phone = updateContactDto.Phone;
            contact.MapLocation = updateContactDto.MapLocation;
            contact.OpenHours = updateContactDto.OpenHours;

            // Veritabanına değişiklikleri kaydet
            _context.Contacts.Update(contact);
            _context.SaveChanges();

            return Ok("Güncelleme işlemi başarılı");
        }

    }
}
