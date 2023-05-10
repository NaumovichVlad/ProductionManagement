namespace BusinessLogic.Interfaces
{
    public interface IService<TDto>
    {
        void Delete(int id);
        void Edit(TDto employeeDto);
        TDto GetById(int id);
        List<TDto> GetList();
        List<TDto> GetSelection(int start, int size, string sortDirection, string sortParameter);
        void Insert(TDto employeeDto);
    }
}