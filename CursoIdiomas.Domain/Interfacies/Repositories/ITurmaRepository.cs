using CursoIdiomas.Domain.Entities;

namespace CursoIdiomas.Domain.Interfacies.Repositories
{
    public interface ITurmaRepository
    {
        void Add(Turma turma);
        void Update(Turma turma);
        void Delete(Turma turma);
        Turma GetById(Guid id);
        IEnumerable<Turma> GetAll(int pageNumber, int pageSize);
        bool ExistTurmaMesmoNumero(string numero);
        bool ExistTurmaMesmoNumeroComIdDiferente(string numero, Guid id);
    }
}
