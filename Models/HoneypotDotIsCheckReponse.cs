
namespace BscTokenSniper.Models
{
	public class HoneypotDotIsCheckResponse
	{
		// public Token token { get; set; }
		// public Token withToken { get; set; }
		// public Summary summary { get; set; }
		// public bool simulationSuccess { get; set; }
		public HoneypotResult? honeypotResult { get; set; }
		// public SimulationResult simulationResult { get; set; }
		// public HolderAnalysis holderAnalysis { get; set; }
		// public string[] flags { get; set; }
		// public ContractCode contractCode { get; set; }
		// public Chain chain { get; set; }
		// public string router { get; set; }
		// public Pair pair { get; set; }
		// public string pairAddress { get; set; }
	}

	// public class Token {
	// 	public string name { get; set; }
	// 	public string symbol { get; set; }
	// 	public int decimals { get; set; }
	// 	public string address { get; set; }
	// 	public int totalHolders { get; set; }
	// }

	// public class Summary {
	// 	public string risk { get; set; }
	// 	public int riskLevel { get; set; }
	// 	public string[] flags { get; set; }
	// }

	public class HoneypotResult
	{
		public bool isHoneypot { get; set; }
	}

	// public class SimulationResult {
	// 	public double buyTax { get; set; }
	// 	public double sellTax { get; set; }
	// 	public double transferTax { get; set; }
	// 	public string buyGas { get; set; }
	// 	public string sellGas { get; set; }
	// }

	// public class HolderAnalysis {
	// 	public string holders { get; set; }
	// 	public string successful { get; set; }
	// 	public string failed { get; set; }
	// 	public string siphoned { get; set; }
	// 	public double averageTax { get; set; }
	// 	public double averageGas { get; set; }
	// 	public double highestTax { get; set; }
	// 	public string highTaxWallets { get; set; }
	// 	public TaxDistribution[] taxDistribution { get; set; }
	// 	public int snipersFailed { get; set; }
	// 	public int snipersSuccess { get; set; }
	// }

	// public class TaxDistribution {
	// 	public int tax { get; set; }
	// 	public int count { get; set; }
	// }

	// public class ContractCode {
	// 	public bool openSource { get; set; }
	// 	public bool rootOpenSource { get; set; }
	// 	public bool isProxy { get; set; }
	// 	public bool hasProxyCalls { get; set; }
	// }

	// public class Chain {
	// 	public string id { get; set; }
	// 	public string name { get; set; }
	// 	public string shortName { get; set; }
	// 	public string currency { get; set; }
	// }

	// public class Pair {
	// 	public PairPair pair { get; set; }
	// 	public string chainId { get; set; }
	// 	public string reserves0 { get; set; }
	// 	public string reserves1 { get; set; }
	// 	public double liquidity { get; set; }
	// 	public string router { get; set; }
	// 	public string createdAtTimestamp { get; set; }
	// 	public string creationTxHash { get; set; }
	// }

	// public class PairPair {
	// 	public string name { get; set; }
	// 	public string address { get; set; }
	// 	public string token0 { get; set; }
	// 	public string token1 { get; set; }
	// 	public string type { get; set; }
	// }

}
