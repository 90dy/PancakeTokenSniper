import { ethers } from "ethers";
import { OpenAI } from "openai";

if (!process.env.OPENAI_API_KEY) {
    throw new Error("OPENAI_API_KEY environment variable is not set");
}

// Initialize OpenAI with your API key
const openai = new OpenAI({
    apiKey: process.env.OPENAI_API_KEY,
});

// Connect to the Ethereum network
const provider = new ethers.JsonRpcProvider(
    "https://bsc-mainnet.core.chainstack.com/cd034565803a5dfea4b380b42debb13d"
);

async function analyzeContract(contractAddress: string) {
    // Get the contract code
    const contractCode = await provider.getCode(contractAddress);

    // Use GPT-4 to analyze the contract code
    const response = await openai.complete({
        engine: "text-davinci-002",
        // tell to analyze the contract code and return a json object with the analysis that correspond to that typescript type:
        // type Analysis = {
        //     trustability: 'high' | 'medium' | 'low';
        //     issues: 'rugpull' | 'honeypot' | 'scam' | 'other';
        //     description: string;
        // };
        prompt:
            `Analyze the following contract code and return a json object with the following properties: trustability, issues, description\n` +
            `The return type:\n` +
            `type Analysis = {\n` +
            `    trustability: 'high' | 'medium' | 'low';\n` +
            `    issues: Array<'rugpull' | 'honeypot' | 'scam' | 'other'>;\n` +
            `    description: string;\n` +
            `};\n` +
            `The contract code:\n` +
            `${contractCode}\n`,
        max_tokens: 200,
    });

    // Print the analysis
    console.log(response.data.choices[0].text.trim());
}
