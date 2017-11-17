
using System.Collections.Generic;

namespace Vidly.Dtos
{
    public class NewRentalDto
    {
        /* IMPORTANT: Having a get set is really important. Else post data will always be null */
        public int CustomerId { get; set; }
        public List<int> MovieIds { get; set; }
    }
}