﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Queries.ProductQ.GetAll
{
    public class GetAllProductQueryRequest : IRequest<GetAllProductQueryResponse>
    {
    }
}
