using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Banco de dados que serve para armazenar as cartas

[CreateAssetMenu(fileName = "Nova Carta", menuName = "Carta", order = 1)]
public class CartaScriptableObject : ScriptableObject
{
    public string NomeDaCarta;

    [TextArea]
    public string DescricaoDaAcao, HistoriaDaCarta;

    public int VidaAtual, DanoDeAtaque, CustoDeMana;

    public Sprite SpritePersonagem;
    public Sprite    SpriteFundo;
}
