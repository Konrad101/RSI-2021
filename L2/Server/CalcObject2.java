import java.rmi.Remote;
import java.rmi.RemoteException;

public interface CalcObject2 extends Remote {
    ResultType calculate2(InputType inputParam) throws RemoteException;
}
