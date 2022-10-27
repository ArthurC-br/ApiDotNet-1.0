using ApiDotNet_1._0.Application.DTOs;
using ApiDotNet_1._0.Application.DTOs.Validations;
using ApiDotNet_1._0.Application.Services.Interfaces;
using ApiDotNet_1._0.Domain.Entites;
using ApiDotNet_1._0.Domain.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet_1._0.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        public PersonService(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<ResultService<PersonDTO>> CreateAsync(PersonDTO personDTO)
        {
            if (personDTO == null) return ResultService.Fail<PersonDTO>("Objeto deve ser informado");

            var result = new PersonDTOValidator().Validate(personDTO);
            if(!result.IsValid) return ResultService.ResquestError<PersonDTO>("Problemas de validade!", result);
            
            var person = _mapper.Map<Person>(personDTO);
            var data = await _personRepository.CreateAsync(person);

            return ResultService.Ok<PersonDTO>(_mapper.Map<PersonDTO>(data));

        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null) return ResultService.Fail<PersonDTO>("Pessoa não encontrada");

            await _personRepository.DeleteAsync(person);
            return ResultService.Ok($"{person.Name} do id: {person.Id} foi deletada!");
        }

        public async Task<ResultService<ICollection<PersonDTO>>> GetAsync()
        {
            var people = await _personRepository.GetPeopleAsync();
            return ResultService.Ok<ICollection<PersonDTO>>(_mapper.Map<ICollection<PersonDTO>>(people));

        }

        public async Task<ResultService<PersonDTO>> GetByIdAsync(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null) return ResultService.Fail<PersonDTO>("Pessoa não encontrada!");

            return ResultService.Ok(_mapper.Map<PersonDTO>(person));
        }
        public async Task<ResultService> UpdateAsync(PersonDTO personDTO)
        {
            if (personDTO == null) return ResultService.Fail("Objeto tem que ser informado!") ;

            var validation = new PersonDTOValidator().Validate(personDTO);
            if (!validation.IsValid) return ResultService.ResquestError("Problemas com a validação dos campos!", validation);

            var person = await _personRepository.GetByIdAsync(personDTO.ID);
            if (person == null) return ResultService.Fail("Pessoa não encontrada!");

            person = _mapper.Map<PersonDTO, Person>(personDTO, person);
            await _personRepository.EditAsync(person);
            return ResultService.Ok("Pessoa editada!");
        }
    }
}
