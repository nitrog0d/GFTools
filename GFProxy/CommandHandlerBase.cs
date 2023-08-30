using GFTools.Common;
using GFTools.Common.Protocol;

namespace GFProxy;

public interface ICommandHandler {
    public bool Handle(ProxyClientBase client, CommandBase command);
}

public abstract class CommandHandlerBase<P, C> : ICommandHandler where P : ProxyClientBase where C : CommandBase {
    public Logger Logger { get; private set; }

    public CommandHandlerBase() {
        Logger = new(GetType().Name);
    }

    public abstract bool Handle(P client, C command);
    bool ICommandHandler.Handle(ProxyClientBase client, CommandBase command) => Handle((P)client, (C)command);
}
