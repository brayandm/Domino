// Esta clase contiene instancias de registrador de implementaciones y el
//que obtiene las instancias de esas implementaciones
public class DependencyContainerRegister
{
    // Este campo contiene el registrador de implementaciones
    public static DependencyContainerOrganizer Register = new DependencyContainerOrganizer();

    // Este campo contiene el obtenedor de instancias
    public static DependencyGetter Getter = new DependencyGetter();
}