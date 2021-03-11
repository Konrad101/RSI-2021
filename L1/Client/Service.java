import java.util.Vector;

public class Service {
    private final String methodName;
    private final Vector<Object> parameters;

    public String getMethodName() {
        return methodName;
    }

    public Vector<Object> getParameters() {
        return parameters;
    }

    public Service(String methodName, Vector<Object> parameters) {
        this.methodName = methodName;
        this.parameters = parameters;
    }
}
