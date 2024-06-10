# Hra Mastermind (Dokumentace)
## Návod k použití

1. **Spusťte aplikaci**
2. **Vyberte barvu**: Klikněte na barevné kruhy ve spodní části pro výběr barvy. Vybraná barva se zobrazí v indikátoru vedle myši.
3. **Vyplňte kolíčky**: Klikněte na kruhy v mřížce, aby se vyplnily vybranou barvou.
4. **Kontrola spravnosti barevného kódu**: Jakmile je řádek vyplněn, klikněte na tlačítko "Check" pro odeslání vašeho pokusu.
5. **Zpětná vazba**: Černé kruhy označují správnou barvu a pozici; červené kruhy označují správnou barvu, ale špatnou pozici.
6. **Výhra**: Pokud uhodnete kód správně do 12 roundu, zobrazí se výherní obrazovka, jinak se zobrazi obrázovka s nádpisem že jste prohrali.

## Popis tříd a metod
### Třída MainWindow
1. Metoda **MainWindow()** je inicializační metoda. Inicializuje komponenty, nastaví barvy, vygeneruje tajný kód, připraví herní pole a nastaví barvu pozadí.
2. Metoda **InitializeColors()** je metoda ktéra má v sobě seznam dostupných barev, které mohou být použity ve hře.
3. Metoda **CreateSecretCode()** vygeneruje tajný kód, který musí uživatel uhodnout.
4. Metoda **PripravPole()** vytvoří mřížku pro hru a přidá tlačítko "Check" pro ověřování kombinací.
5. Metoda **AddColorPin()** přidá obarevné kolíčky do mřížky.
6. Metoda **AddEvaluationPins()** obarvi kolíčky pro zpětnou vazbu (černé a červené kolíčky) do mřížky.
7. Metoda **SelectColor()** nastaví vybranou barvu na základě kliknutí na barvu.
8. Metoda **FillColor()** vyplní kolíček vybranou barvou, pokud je ve správném sloupciyplní kolíček vybranou barvou, pokud je ve správném sloupci.  
9. Metoda **Check()** ověří kombinaci, kterou hráč zadal, a přidá zpětnou vazbu (černé a červené kolíčky). Pokud hráč vyhrál nebo prohrál, zobrazí příslušné okno.
10. Metoda **EvaluatePins()** vyhodnotí kombinaci, kterou hráč zadal, a vrátí pole obsahující černé a červené kolíčky jako zpětnou vazbu.
11. Metoda **Window_MouseMove()** přesune ukazatel barvy podle pohybu myši nad herní mřížkou.

### Třída FinalWindow
1. Metoda **FinalWindow** zobrazí nové okno s textovou zprávou, které vypiše hrač výhral nebo prohral.
