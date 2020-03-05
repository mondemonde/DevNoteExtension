using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public interface ICommandHandler<TCommand>
    {
        void Handle(TCommand command);
    }
}
