import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;

public class CalcObjImpl2 extends UnicastRemoteObject implements CalcObject2 {

    protected CalcObjImpl2() throws RemoteException {
        super();
    }

    @Override
    public ResultType calculate2(InputType inputParam) throws RemoteException {
        double firstValue, secondValue;
        ResultType result = new ResultType();

        firstValue = inputParam.getX1();
        secondValue = inputParam.getX2();

        result.resultDescription = "Operacja " + inputParam.operation;

        switch (inputParam.operation) {
            case "add":
                result.result = firstValue + secondValue;
                break;
            case "sub":
                result.result = firstValue - secondValue;
                break;
            default:
                result.result = 0;
                result.resultDescription = "Podano zla operacje";
        }

        return result;
    }
}
