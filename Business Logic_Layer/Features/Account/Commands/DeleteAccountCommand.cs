using AutoMapper;
using Business_Logic_Layer.Features.Account.Models;
using Data_Access_Layer.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Features.Account.Commands
{
    public class DeleteAccountCommand : IRequest
    {
        public int Id { get; init; }

        public DeleteAccountCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand>
    {
        private readonly ContextDb _context;
        

        public DeleteAccountCommandHandler(ContextDb context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
           
        }

       

        public async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(i => i.Id == request.Id);
            if (account == null) throw new ArgumentNullException(nameof(account));

            _context.Accounts.Remove(account);
            _context.SaveChanges();
        }
    }
}
