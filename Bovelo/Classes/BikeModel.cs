using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

public class BikeModel
{
	public string type;
	public int price;
	public DateTime totalTime;


	public BikeModel(string type)
	{
		this.type = type;
		//this.price = getBikePartsList().price;
		//this.totalTime = getBikePartsList().totalTime;
	}
}
