using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DataBaseOpeOperations;

namespace WebApi.Applications.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenresQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GenreQueryViewModel> Handle()
        {
            var genres = _context.Genres.Where(x => x.IsActive).OrderBy(x => x.Id);
            List<GenreQueryViewModel> result = _mapper.Map<List<GenreQueryViewModel>>(genres);
            return result;
        }
    }

    public class GenreQueryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}