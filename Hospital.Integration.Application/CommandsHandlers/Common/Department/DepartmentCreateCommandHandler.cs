using Hospital.Integration.Application.Abstractions.Data;
using Hospital.Integration.Application.Factories.Common;
using MediatR;

namespace Hospital.Integration.Application.Handlers.Common;

public class DepartmentCreateCommand : IRequest<string>
{
    public string? Id { get; init; } = Guid.NewGuid().ToString();

    public string Name { get; init; }

    public bool Active { get; init; }
}

public class DepartmentCreateCommandHandler : IRequestHandler<DepartmentCreateCommand, string>
{
    private readonly IUnitOfWork _uow;
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentCreateCommandHandler(
        IUnitOfWork uow,
        IDepartmentRepository departmentRepository)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
    }

    //public async Task<string> Handle(string request, CancellationToken cancellationToken)
    //{
    //    var pessoa = new Pessoa { Nome = request.Nome, Idade = request.Idade, Sexo = request.Sexo };

    //    try
    //    {
    //        pessoa = await _repository.Add(pessoa);

    //        await _mediator.Publish(new PessoaCriadaNotification { Id = pessoa.Id, Nome = pessoa.Nome, Idade = pessoa.Idade, Sexo = pessoa.Sexo });

    //        return await Task.FromResult("Pessoa criada com sucesso");
    //    }
    //    catch (Exception ex)
    //    {
    //        await _mediator.Publish(new PessoaCriadaNotification { Id = pessoa.Id, Nome = pessoa.Nome, Idade = pessoa.Idade, Sexo = pessoa.Sexo });
    //        await _mediator.Publish(new ErroNotification { Excecao = ex.Message, PilhaErro = ex.StackTrace });
    //        return await Task.FromResult("Ocorreu um erro no momento da criação");
    //    }

    //}

    public async Task<string> Handle(DepartmentCreateCommand departmentCreateCommand, CancellationToken cancellationToken)
    {
        _uow.BeginTransaction();
        var department = DepartmentFactory.FromCreate(departmentCreateCommand);
        var id = await _departmentRepository.CreateAsync(department);
        _uow.Commit();

        return id;
    }
}
