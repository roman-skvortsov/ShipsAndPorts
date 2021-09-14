using AutoMapper;
using ShipsAndPorts.Core.Models;
using ShipsAndPorts.Core.Models.ApiModels;
using ShipsAndPorts.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipsAndPorts.Services.Services
{
    public class PortService : BaseService<Port, PortApiModel>, IPortService
    {
        public PortService(IMapper mapper): base(mapper)
        {

        }
    }
}
