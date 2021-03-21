import java.io.Serializable;

public class InputType implements Serializable {
    private static final long SERIAL_VERSION_UID = 101L;
    public String operation;
    private final double x1;
    private final double x2;

    public InputType(double x1, double x2) {
        this.x1 = x1;
        this.x2 = x2;
    }

    public double getX1() {
        return x1;
    }

    public double getX2() {
        return x2;
    }
}
