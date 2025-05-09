export function riskClassColor(risk: number): string {
  if (risk < 30) return 'text-green-500';
  if (risk < 70) return 'text-yellow-500';
  return 'text-red-500';
}
